import * as React from "react";
import Input from "retail-ui/components/Input";

export class CurrencyInputProps {
    placeholder?: string;
    value: number;
    emptyValue?: number;
    onChange: (value: number) => void;
}

class CurrencyInputState {
    value: string;
}

export default class CurrencyInput extends React.Component<CurrencyInputProps, CurrencyInputState> {
    private selectionStart: number = 0;
    private selectionEnd: number = 0;
    private innerInput?: HTMLInputElement;


    constructor(props: CurrencyInputProps, context?: any){
        super(props, context);
        this.state = {
            value:this.formatDisplayValue(this.props.value)
        };
    }

    static defaultProps = {
        emptyValue: null as number
    };
    
    render() {
        return (
            <Input
                placeholder={this.props.placeholder}
                value={this.state.value}
                onChange={this.handleChange}
                ref={this.handleRef as any}
                onKeyDown={(event) => {
                    const target = event.target;
                    if (target instanceof HTMLInputElement) {
                        this.selectionStart = target.selectionStart;
                        this.selectionEnd = target.selectionEnd;
                        /*if (onKeyDown !== null && onKeyDown !== undefined) {
                            onKeyDown(event, ...rest);
                        }*/
                    }
                }}
            />
        );
    }
    
    handleRef = (element: HTMLInputElement): void => {
        this.innerInput = element
    };

    handleChange = (event: any, value: string) :void =>{
        var formatted = value.replace(/[\.\/бю]/g, ",");
        if (formatted.match(/^\d{0,13}(\,\d{0,2})?$/)) {
            if (this.state.value !== formatted) {
                const current = this.parseCurrency(this.state.value, this.props.emptyValue);
                const next = this.parseCurrency(formatted, this.props.emptyValue);
                this.setState({value: formatted}, () => {
                    if (current !== next) {
                        this.props.onChange(next);
                    }
                });
            }
        }
        else {


            if (this.innerInput != null) {
                this.setState({}, () => {
                    if (this.innerInput != null) {
                        this.innerInput.setSelectionRange(this.selectionStart, this.selectionEnd);
                    }
                });
            }
        }

    };
    
    formatDisplayValue = (value: number): string => {
        return (value || "").toString().split("").join("");
    };
    
    parseCurrency = (value: string, emptyValue: number): number => {
        const parsed = parseFloat((value || "").replace(/\s/g,"").replace(",", "."));
        return isNaN(parsed) ? emptyValue : parsed;
    };
}