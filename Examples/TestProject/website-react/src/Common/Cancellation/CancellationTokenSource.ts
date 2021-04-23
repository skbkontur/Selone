import CancellationToken from "./CancellationToken";
import CancellationTokenRegistration from "./CancellationTokenRegistration";

export default class CancellationTokenSource {
    constructor();
    constructor(timeout: number);
    constructor(canceled: boolean);
    constructor(arg?: boolean | number) {
        if (arg == null) {
            this.state = State.NotCanceled;
        } else if (typeof arg === "number") {
            this.state = State.NotCanceled;
            setTimeout(() => this.cancel(), arg)
        } else {
            this.state = arg ? State.NotifyingComplete : State.CannotBeCanceled;
        }
    }

    get token(): CancellationToken {
        return new CancellationToken(this);
    };

    get canBeCanceled(): boolean {
        return this.state !== State.CannotBeCanceled;
    };

    get isCancellationRequested(): boolean {
        return this.state > State.NotCanceled;
    }

    cancel = () => {
        if (!this.isCancellationRequested) {
            this.state = State.Notifying;
            try {
                for (const action of this.callbacks) {
                    action();
                }
            } finally {
                this.state = State.NotifyingComplete;
            }
        }
    };

    cancelAfter = (timeout: number) => {
        if (!this.isCancellationRequested) {
            setTimeout(this.cancel, timeout);
        }
    };

    private state: State;
    private callbacks: (() => void)[] = [];

    internalRegiser(action: () => void): CancellationTokenRegistration {
        if (this.isCancellationRequested) {
            action();
            return new CancellationTokenRegistration();
        }

        this.callbacks.push(action);
        return new CancellationTokenRegistration(this.callbacks, action);
    }
}

enum State{
    CannotBeCanceled = 0,
    NotCanceled = 1,
    Notifying = 2,
    NotifyingComplete = 3
}