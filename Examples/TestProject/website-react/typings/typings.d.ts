interface Window {
    onunhandledrejection: (this: Window, event: PromiseRejectionEvent, promise: PromiseLike<any>) => any
    addEventListener(type: "unhandledrejection", listener: (this: Window, ev: PromiseRejectionEvent) => any, useCapture?: boolean): void;
}