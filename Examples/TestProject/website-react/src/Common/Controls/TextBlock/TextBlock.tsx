import * as React from "react";
import ComponentHelper from "src/Common/ComponentHelper";
import {AccentColor, AccentColors, AccentColorType} from "src/Common/Colors";

const styles = require("./TextBlock.less");

export enum TextBlockBehavior {
    Default,
    Ellipsis,
    WordWrap,
}

export interface TextBlockProps {
    behavior: TextBlockBehavior;
    width?: number | string;
    height?: number | string;
    color?: AccentColor;
    children: React.ReactNode
}

export default class TextBlock extends React.Component<TextBlockProps, {}> {
    render() {
        const className = ComponentHelper.getClassNames({
            [styles.ellipsis]: this.props.behavior === TextBlockBehavior.Ellipsis,
            [styles.wordWrap]: this.props.behavior === TextBlockBehavior.WordWrap,
        })
        let style = {
            width: this.props.width,
            height: this.props.height,
            color: this.props.color ? AccentColors[this.props.color][AccentColorType.Text] : null
        };
        return (
            <span className={className} style={style}>
                {this.props.children}
            </span>
        );
    }
}