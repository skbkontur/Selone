import * as React from "react";

export class RenderErrorMessage {
}

export interface ValidationContainerProps {
    children?: any;
    onValidationUpdated?: (isValid?: boolean) => void;
    scrollOffset?: number;
}

export class ValidationContainer extends React.Component<ValidationContainerProps, any> {
    submit(withoutFocus?: boolean): Promise<void>;
    validate(withoutFocus?: boolean): Promise<boolean>;
}

export interface ValidationInfo {
    type?: "immediate" | "lostfocus" | "submit";
    level?: "error" | "warning";
    message: React.ReactNode;
}

export interface ValidationWrapperV1Props {
    children?: React.ReactElement<any>;
    validationInfo: ValidationInfo;
    renderMessage?: RenderErrorMessage;
}

export class ValidationWrapperV1 extends React.Component<ValidationWrapperV1Props, any> {
}

export function tooltip(pos: string): RenderErrorMessage;
export function text(pos?: string): RenderErrorMessage;