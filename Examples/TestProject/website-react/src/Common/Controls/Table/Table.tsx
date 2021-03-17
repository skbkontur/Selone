import * as React from "react";
import ComponentHelper from "src/Common/ComponentHelper";
import {Loader} from "src/Common/Controls";
import {Kebab} from "src/Common/Controls";

const styles = require("./Table.less");

interface TableBodyProps {
    children?: React.ReactNode
}

class TableBody extends React.Component<TableBodyProps> {
    render() {
        return <tbody>{this.props.children}</tbody>;
    }
}

interface TableHeaderProps {
    children?: React.ReactNode
}

class TableHeader extends React.Component<TableHeaderProps> {
    render() {
        return <thead><tr>{this.props.children}</tr></thead>;
    }
}

interface TableTdProps {
    children?: React.ReactNode;
    align?: "left" | "right";
}

class TableTd extends React.Component<TableTdProps> {
    static defaultProps = {
        align: "left"
    };

    render() {
        const classes = {
            [styles.td]: true,
            [styles.textAlignLeft]: this.props.align === "left",
            [styles.textAlignRight]: this.props.align === "right"
        };
        const classNames = ComponentHelper.getClassNames(classes);

        return (
            <td className={classNames}>
                {this.props.children}
            </td>
        );
    }
}

interface TableTdKebabProps {
    children?: React.ReactNode;
    disabled?: boolean;
}

class TableTdKebab extends React.Component<TableTdKebabProps> {
    //todo stopPropagation здесь выглядит как костылек, кажется это должно уехать в Kebab
    render() {
        const hesItems = !!React.Children.toArray(this.props.children).filter(x => x).length;
        return (
            <td className={styles.td}>
                {hesItems &&
                <div className={styles.kebab} onClick={e => e.stopPropagation()}>
                    <Kebab size={"large"} disabled={this.props.disabled}>
                        {this.props.children}
                    </Kebab>
                </div>
                }
            </td>
        );
    }
}

interface TableThProps {
    children?: React.ReactNode;
    align?: "left" | "right";
    width?: number | string;
}

class TableTh extends React.Component<TableThProps> {
    static defaultProps = {
        align: "left"
    };

    render() {
        const classes = {
            [styles.th]: true,
            [styles.textAlignLeft]: this.props.align === "left",
            [styles.textAlignRight]: this.props.align === "right"
        };
        const classNames = ComponentHelper.getClassNames(classes);

        const style = {
            width: this.props.width
        };

        return (
            <th className={classNames} style={style}>
                {this.props.children}
            </th>
        );
    }
}

class TableThKebab extends React.Component {
    render() {
        return (
            <th className={styles.thKebab}/>
        );
    }
}

interface TableTrProps {
    onClick?: () => void;
    children?: React.ReactNode;
    loading?: boolean;
    changed?: boolean;
    selected?: boolean
}

class TableTr extends React.Component<TableTrProps> {
    private element: HTMLTableRowElement;

    render() {
        const classes = {
            [styles.tr]: true,
            [styles.loading]: this.props.loading,
            [styles.changed]: this.props.changed,
            [styles.selected]: this.props.selected,
            [styles.clickable]: !!this.props.onClick
        };
        const classNames = ComponentHelper.getClassNames(classes);

        const tds = this.props.loading
            ? this.getTrWithLoader(this.props.children)
            : this.props.children;

        return <tr className={classNames} ref={this.handleRef} onClick={!this.props.loading && this.props.onClick}>{tds}</tr>;
    }

    private getTrWithLoader = (tds: React.ReactNode): React.ReactNode => {
        return React.Children.map(tds, (td, i) => {
            return i === 0 ? this.getTdWithLoader(td as React.ReactElement<any>) : td;
        });
    };

    private getTdWithLoader = (td: React.ReactElement<any>): React.ReactNode => {
        if (td.type !== Table.Td) {
            throw new Error("Not a Table.Td:\n" + td.type);
        }

        const height = this.element && (this.element.getBoundingClientRect().height - 1);
        const loader = (
            <Loader key={"loader"} active={true} caption="Загрузка" className={styles.loader} type="mini">
                <div style={{height}}/>
            </Loader>
        );

        return React.cloneElement(td, null, [loader, ...td.props.children]);
    };

    private handleRef = (element: HTMLTableRowElement) => {
        this.element = element;
    };
}

interface TableProps {
    children?: React.ReactNode;
}

export default class Table extends React.Component<TableProps> {
    static Header: typeof TableHeader = TableHeader;
    static Body: typeof TableBody = TableBody;
    static Tr: typeof TableTr = TableTr;
    static Td: typeof TableTd = TableTd;
    static Th: typeof TableTh = TableTh;
    static TdKebab: typeof TableTdKebab = TableTdKebab;
    static ThKebab: typeof TableThKebab = TableThKebab;

    render() {
        return <table className={styles.table}>{this.props.children}</table>;
    }
}
