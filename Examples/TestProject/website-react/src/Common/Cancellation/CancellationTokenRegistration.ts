export default class CancellationTokenRegistration {
    constructor(actions?: (()=>void)[], action?:()=>void) {
        this.actions = actions;
        this.action = action;
    }

    dispose = (): void => {
        if (this.actions && this.action) {
            const index = this.actions.indexOf(this.action);
            if (index !== -1) {
                this.actions.splice(index, 1);
            }
        }
    };
    
    private readonly actions: (()=>void)[];
    private readonly action: () => void;
}