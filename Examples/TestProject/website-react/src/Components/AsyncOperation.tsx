import * as React from "react";
import {Gapped, Button, Spinner} from "src/Common/Controls";
import PromiseHelper from "src/Common/TypeHelpers/PromiseHelper";

export interface AsyncOperationProps {
}

export interface AsyncOperationState {
    operation: OperationState;
}

enum OperationState {
    None,
    InProgress,
    Done
}

export default class AsyncOperation extends React.Component<AsyncOperationProps, AsyncOperationState> {
    state: AsyncOperationState = {
        operation: OperationState.None,
    };

    render() {
        return (
            <div style={{display: "inline-block"}}>
                <Gapped vertical>
                    <Gapped>
                        <Button
                            data-tid="ExecuteButton"
                            use={"success"}
                            disabled={this.state.operation != OperationState.None}
                            onClick={this.handleExecuteButtonClick}
                        >
                            Выполнить
                        </Button>
                        <Button
                            data-tid="ResetButton"
                            use={"danger"}
                            disabled={this.state.operation != OperationState.Done}
                            onClick={this.handleResetButtonClick}
                        >
                            Отменить
                        </Button>
                    </Gapped>
                    <div style={{background: "lightgrey", height: 2}}/>
                    {this.renderState()}
                </Gapped>
            </div>
        );
    }

    private renderState = () => {
        if (this.state.operation == OperationState.InProgress) {
            return (
                <Spinner data-tid="Spinner" type={"mini"} caption={"Выполняется"}/>
            );
        }

        if (this.state.operation == OperationState.Done) {
            return (
                <div data-tid="Result"><b>Успешно выполнено</b></div>
            );
        }

        return null;
    };

    private handleExecuteButtonClick = () => {
        this.setState({operation: OperationState.InProgress}, async () => {
            await PromiseHelper.delay(1000 + 2000 * Math.random());
            this.setState({operation: OperationState.Done});
        });
    };

    private handleResetButtonClick = () => {
        this.setState({operation: OperationState.None});
    };
}