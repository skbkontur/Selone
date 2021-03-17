// @flow
import * as React from "react";
import { ComboBoxOld } from "src/Common/Controls";


export type SimpleComboBoxProps<TInfo, TValue> = {
    source: (query: string) => Promise<TInfo[]>,
    valueToInfo: (id: TValue) => TInfo | Promise<TInfo>,
    infoToValue: (id: TInfo) => TValue,
    infoToString: (info: TInfo) => string,
    value: TValue,
    onChange: (value: TValue, info: TInfo) => void,
    render: (info: TInfo, state: null|"hover") => string | React.ReactElement<any> | null,
    renderValue?: (info: TInfo) => string | React.ReactElement<any> | null,
    placeholder?: string,
    renderNotFound?: string,
    width?: number | string,
    disabled?: boolean,
    error?: boolean,
};

export default class SimpleComboBox<TInfo, TValue> extends React.Component<SimpleComboBoxProps<TInfo, TValue>,{}> {
    props: SimpleComboBoxProps<TInfo, TValue>;
    items: TInfo[] | null;
    actualInfo: TInfo;

    render(): React.ReactElement<any> {
        const {value, onChange, render, placeholder, renderNotFound, width, disabled, error} = this.props;
        return (
            <ComboBoxOld
                width={width || 450}
                source={query => this.source(query)}
                value={value}
                info={this.valueToInfo}
                renderValue={(value, info) => this.renderValue(value, info)}
                renderItem={(value, info, state) => render(info, state)}
                valueToString={value => this.infoToString(value)}
                renderNotFound={renderNotFound || "Ничего не найдено"}
                onChange={(e, val) => onChange(val, this.getValueForOnChange(val))}
                placeholder={placeholder}
                disabled={disabled}
                error={error}
            />
        );
    }

    focus() {
    }

    getValueForOnChange = (value: TValue): TInfo => {
        const {infoToValue} = this.props;
        if (!value) {
            return null;
        }
        if (this.items && this.items.length !== 0) {
            const item = this.items.find(item => infoToValue(item) === value);
            return item;
        }
        if (this.actualInfo && infoToValue(this.actualInfo) === value) {
            return this.actualInfo;
        }
        return null;
    };

    infoToString = (value: TValue): string => {
        const {infoToString, infoToValue} = this.props;
        if (this.items && this.items.length !== 0) {
            const item = this.items.find(item => infoToValue(item) === value);
            if (item === null || item === undefined) {
                return "";
            }
            return infoToString(item);
        }
        if (this.actualInfo && infoToValue(this.actualInfo) === value) {
            return infoToString(this.actualInfo);
        }
        return "";
    };

    createInfo = (items: TInfo[]): { values: TValue[], infos: TInfo[] } | null => {
        const {infoToValue} = this.props;
        if (!items) {
            return null;
        }
        return {
            values: items.map(item => infoToValue(item)),
            infos: items,
        };
    };

    renderValue = (value: TValue, info: TInfo): string | React.ReactElement<any> | null => {
        const {render, renderValue} = this.props;

        if (info !== null) {
            if (renderValue != null) {
                return renderValue(info);
            }
            return render(info, null);
        }
        if (value !== null) {
            return <span>Загрузка...</span>;
        }
        return null;
    };

    source = async (query: string): Promise<{ values: TValue[], infos: TInfo[] }> => {
        const {source} = this.props;
        const results = await source(query);
        this.items = results;
        return this.createInfo(results);
    };

    valueToInfo = async (id: TValue): Promise<TInfo> => {
        const {valueToInfo, infoToValue} = this.props;
        if (id === undefined || id === null) {
            return null;
        }
        if (this.items && this.items.length !== 0) {
            return this.items.find(item => infoToValue(item) === id) || null;
        }
        const result = await valueToInfo(id);
        this.actualInfo = result;
        return result;
    };
}