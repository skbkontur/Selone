import CancellationTokenSource from "src/Common/Cancellation/CancellationTokenSource";
import CancellationToken from "src/Common/Cancellation/CancellationToken";

export default class PromiseHelper {
    static delay = (timeout: number): Promise<void> => {
        return new Promise((resolve) => setTimeout(() => resolve(), timeout)) as Promise<any>;
    };

    static resolveAfter = <T>(promise: Promise<T>, timeout: number): Promise<T> => {
        return Promise.all([promise, PromiseHelper.delay(timeout)]).then(x => x[0]);
    };

    static rejectAfter = <T>(promise: Promise<T>, timeout: number): Promise<T> => {
        const source = new CancellationTokenSource();
        source.cancelAfter(timeout);
        return PromiseHelper.withCancellation(promise, source.token);
    };

    static continueAfter = <T>(promise: Promise<T>, timeout: number): Promise<T> => {
        return PromiseHelper.delay(timeout).then(() => promise);
    };

    static fromToken = (cancellationToken: CancellationToken): Promise<void> => {
        return new Promise((resolve, reject) => {
            cancellationToken.register(() => {
                try {
                    cancellationToken.throwIfCancellationRequested();
                } catch (exception) {
                    reject(exception);
                }
            });
        }) as Promise<any>;
    };

    static withCancellation = <T>(promise: Promise<T>, cancellationToken: CancellationToken): Promise<T> => {
        return Promise.race([PromiseHelper.fromToken(cancellationToken), promise]) as Promise<T>;
    };
}