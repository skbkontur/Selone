import * as React from "react";
import {Button, Gapped} from "src/Common/Controls";
import ObjectHelper from "src/Common/TypeHelpers/ObjectHelper";

const styles = require("./Filter.less");

class Line extends React.Component {
    render() {
        return <div>{this.props.children}</div>;
    }
}

export interface FilterProps<T> {
    onChange: (filterData: T) => void;
    initialFilterData: T;
    currentFilterData: T;
    appliedFilterData: T;
}

export default class Filter<T> extends React.Component<FilterProps<T>> {
    static Line: typeof Line = Line;

    render() {
        const children = React.Children.toArray(this.props.children);
        const linesCount = children.filter((child: React.ReactChild) => {
            const element = child as React.ReactElement<any>;
            return element.type && element.type === Filter.Line;
        }).length;

        if (0 < linesCount && linesCount < children.length) {
            throw new Error("Либо все Children должны являться Line, либо ни одного");
        }

        const lines = linesCount === 0 ? [children] : children;

        return (
            <div className={styles.filter}>
                <Gapped vertical gap={10}>
                    <Gapped gap={10}>
                        {lines[0]}
                        <Button
                            use={"success"}
                            data-tid="ApplyButton"
                            width={103}
                            size={"small"}
                            disabled={ObjectHelper.isEqual(this.props.currentFilterData, this.props.appliedFilterData)}
                            onClick={() => this.props.onChange(this.props.currentFilterData)}
                        >
                            Применить
                        </Button>
                        <Button
                            data-tid="CancelButton"
                            disabled={ObjectHelper.isEqual(this.props.initialFilterData, this.props.currentFilterData) && ObjectHelper.isEqual(this.props.initialFilterData, this.props.appliedFilterData)}
                            onClick={() => this.props.onChange(this.props.initialFilterData)}
                            width={94}
                            size={"small"}
                        >
                            Отменить
                        </Button>
                    </Gapped>
                    {lines.slice(1)}
                </Gapped>
            </div>
        );
    }
}