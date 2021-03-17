import * as React from "react";
import {ValidationWrapperV1, ValidationInfo, tooltip} from "react-ui-validations/index";
import DatePicker, {DatePickerOldProps} from "retail-ui/components/DatePickerOld/DatePickerOld";

//TODO (byTimo) перевести DatePicker на нормальную версию
export interface DateInputProps extends DatePickerOldProps {
    validationInfo: ValidationInfo;
}

export class DateInput extends React.Component<DateInputProps, any> {
    render() {
        const {validationInfo, ...datePickerProps} = this.props;
        return (
            <ValidationWrapperV1 validationInfo={validationInfo} renderMessage={tooltip("top left")}>
                <DatePicker {...datePickerProps}/>
            </ValidationWrapperV1>
        );
    }
}
