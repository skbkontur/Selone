import * as React from "react";

export interface GappedProps {
    children: React.ReactNode;
    gap?: number;
    vertical?: boolean;
    alignItems?: React.CSSProperties["alignItems"]
}

export default class Gapped extends React.Component<GappedProps> {
    static defaultProps: Partial<GappedProps> = {
        gap: 10
    };

    render() {
        return (
            <div
                style={{
                    display: "flex",
                    flexDirection: this.props.vertical ? "column" : "row",
                    alignItems: this.props.alignItems
                }}
            >
                {React.Children.map(this.props.children, (child, index) => <div style={this.getItemStyles(index)}>{child}</div>)}
            </div>
        );
    }

    private getItemStyles = (index: number) => {
        let margin = this.props.vertical ? "marginTop" : "marginLeft";
        let gap = index === 0 ? undefined : this.props.gap;
        return {[margin]: gap};
    }
}