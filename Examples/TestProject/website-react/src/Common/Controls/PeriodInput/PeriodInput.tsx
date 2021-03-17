import * as React from "react";
import { DateInput } from "src/Common/Controls";
import Period from "src/Common/Types/Period";
import { ValidationInfo } from "react-ui-validations/index";
import DateHelper from "src/Common/TypeHelpers/DateHelper";
const styles = require("./PeriodInput.less");

export class PeriodInputProps {
    onChange: (period: Period) => void;
    value: Period;
}

class PeriodInputState {
}

export default class PeriodInput extends React.Component<PeriodInputProps, PeriodInputState> {
    render() {
        return (<span>
                    <span className={styles.datepicker__separator}>с</span>
                    <DateInput data-tid="DateFrom" value={this.props.value.from} onChange={this.handleDateFromChange} validationInfo={this.validationFrom()}/>
                    <span className={styles.datepicker__separator}>по</span>
                    <DateInput data-tid="DateTo" value={this.props.value.to} onChange={this.handleDateToChange} validationInfo={this.validationTo()}/>
                </span>);
    }
    
    validationFrom = (): ValidationInfo => {
        if (this.props.value.from && this.props.value.to && this.props.value.from.getTime() > this.props.value.to.getTime()) {
            return {message: `Дата начала периода не может быть позже даты его окончания. Укажите дату не позднее ${DateHelper.momentFormat(this.props.value.to)}.`};
        }
        return null;
    };
    
    validationTo = (): ValidationInfo => {
        if (this.props.value.from && this.props.value.to && this.props.value.from.getTime() > this.props.value.to.getTime()) {
            return {message: `Дата окончания периода не может быть раньше даты его начала. Укажите дату не ранее ${DateHelper.momentFormat(this.props.value.from)}.`};
        }
        return null;
    };

    handleDateFromChange = (e: any, value: Date) => {
        this.props.onChange(new Period(value, this.props.value.to));
    };

    handleDateToChange = (e: any, value: Date) => {
        this.props.onChange(new Period(this.props.value.from, value));
    };
}