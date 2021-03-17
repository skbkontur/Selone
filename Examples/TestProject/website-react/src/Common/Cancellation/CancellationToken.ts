import CancellationTokenSource from "./CancellationTokenSource";
import CancellationTokenRegistration from "./CancellationTokenRegistration";
import OperationCanceledError from "./OperationCanceledError";

export default class CancellationToken {
    constructor();
    constructor(canceled: boolean);
    constructor(source: CancellationTokenSource);
    constructor(value?: CancellationTokenSource | boolean) {
        if (value == null) {
            this.source = null;
        } else if (value instanceof CancellationTokenSource) {
            this.source = value;
        } else {
            this.source = new CancellationTokenSource(value);
        }
    }

    get canBeCanceled(): boolean {
        return this.source != null && this.source.canBeCanceled;
    };

    get isCancellationRequested(): boolean {
        return this.source != null && this.source.isCancellationRequested;
    };

    static get none(): CancellationToken {
        return new CancellationToken();
    }

    throwIfCancellationRequested = (): void => {
        if (this.isCancellationRequested) {
            this.throwOperationCanceledError();
        }
    };
    
    throwOperationCanceledError = (): void => {
        throw new OperationCanceledError("Operation canceled", this);
    };

    register = (action: () => void): CancellationTokenRegistration => {
        if (!action) {
            throw new Error("action is empty");
        }

        if (!this.source) {
            return new CancellationTokenRegistration();
        }
        
        return this.source.internalRegiser(action);
    };

    private readonly source: CancellationTokenSource;
}

