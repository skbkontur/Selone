declare module "*.svg" {
    export interface SvgComponentProps {
        width?: number | string;
        height?: number | string;
        className?: string;
    }

    export default class SvgComponent extends React.Component<SvgComponentProps, {}> {
    }
}