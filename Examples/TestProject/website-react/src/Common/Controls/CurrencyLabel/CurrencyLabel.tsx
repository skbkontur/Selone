import * as React from "react";
import {AccentColor, AccentColors, AccentColorType} from "src/Common/Colors";
import CurrencyLabelInternal from "@skbkontur/react-ui/CurrencyLabel";

export interface CurrencyLabelProps {
    value: number;
    fractionDigits?: number;
    zeroCoalesce?: React.ReactNode;
    nullCoalesce?: React.ReactNode;
    color?: AccentColor;
    negativeColor?: AccentColor;
    zeroColor?: AccentColor;
    nullColor?: AccentColor;
}

export default class CurrencyLabel extends React.Component<CurrencyLabelProps, {}> {
    render() {
        const accentColor = this.getColor(this.props.value) || this.props.color;
        const style = accentColor != null ? {color: AccentColors[accentColor][AccentColorType.Text]} : {};
        return (
            <span style={style}>
                {this.renderValue()}
            </span>
        )
    }

    renderValue = () => {
        const mdash = "—";
        const {value, nullCoalesce, zeroCoalesce} = this.props;
        if (value == null) {
            return (
                <span>
                    {nullCoalesce === true ? mdash : nullCoalesce}
                </span>
            );
        }
        if (value === 0 && zeroCoalesce !== undefined && zeroCoalesce !== false) {
            return (
                <span>
                    {zeroCoalesce === true ? mdash : zeroCoalesce}
                </span>
            );
        }
        return (
            <CurrencyLabelInternal value={this.props.value} fractionDigits={this.props.fractionDigits} currencySymbol={null}/>
        )
    };

    private getColor = (value: number): AccentColor => {
        if (value == null) {
            return this.props.nullColor;
        }
        if (value === 0) {
            return this.props.zeroColor;
        }
        if (value < 0) {
            return this.props.negativeColor;
        }
        return this.props.color;
    };
}