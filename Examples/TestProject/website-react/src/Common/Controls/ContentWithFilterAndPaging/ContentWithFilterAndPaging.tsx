import * as React from "react";
import {Paging, Gapped, Spinner, Center, Loader} from "src/Common/Controls";
import PromiseHelper from "src/Common/TypeHelpers/PromiseHelper";
import ObjectHelper from "src/Common/TypeHelpers/ObjectHelper";
import CancellationToken from "src/Common/Cancellation/CancellationToken";
import CancellationTokenSource from "src/Common/Cancellation/CancellationTokenSource";

const styles = require("./ContentWithFilterAndPaging.less");

export interface FilterResult<TData> {
    data: TData;
    takeCount: number;
    totalCount: number;
    any: boolean;
}

export interface ContentWithFilterAndPagingProps<TFilter, TData> {
    showContent?: boolean;
    filter: (filter: TFilter, skip: number, take: number, cancellation: CancellationToken) => Promise<FilterResult<TData>>;
    itemsPerPage?: number;
    activePage?: number;
    filterValue: TFilter;
    renderFilter: (value: TFilter) => React.ReactNode;
    renderContent: (value: TData) => React.ReactNode;
    renderNothing: () => React.ReactNode;
    renderNotFound: () => React.ReactNode;
    onChange: (data: TData, activePage: number) => void;
}

export interface ContentWithFilterAndPagingState<TFilter, TData> {
    filter: TFilter
    data?: TData
    inited: boolean;
    loading: boolean;
    activePage: number;
    pagesCount: number;
    any: boolean;
}

export default class ContentWithFilterAndPaging<TFilter, TData> extends React.Component<
    ContentWithFilterAndPagingProps<TFilter, TData>,
    ContentWithFilterAndPagingState<TFilter, TData>
    > {
    static defaultProps = {
        itemsPerPage: 50,
        activePage: 1,
    };

    private cancellationTokenSource: CancellationTokenSource;

    constructor(props: ContentWithFilterAndPagingProps<TFilter, TData>, context: any) {
        super(props, context);
        this.state = {
            filter: this.props.filterValue,
            loading: false,
            activePage: 1,
            pagesCount: 0,
            inited: false,
            any: false
        };
    }

    componentDidMount() {
        this.runFilter(this.props.filterValue, this.props.activePage);
    }

    componentWillReceiveProps(nextProps: Readonly<ContentWithFilterAndPagingProps<TFilter, TData>>) {
        if (!ObjectHelper.isEqual(this.props.filterValue, nextProps.filterValue)
            || (this.state.loading && this.props.activePage !== nextProps.activePage)
            || (!this.state.loading && this.state.activePage !== nextProps.activePage)) {
            this.runFilter(nextProps.filterValue, nextProps.activePage);
        }
    }

    runFilter = (filterValue: TFilter, activePage: number) => {
        this.cancellationTokenSource && this.cancellationTokenSource.cancel();
        this.cancellationTokenSource = new CancellationTokenSource();
        this.setState({loading: true}, async () => {
            const promise = this.filterInternal(filterValue, activePage, this.cancellationTokenSource.token);
            const result = await PromiseHelper.withCancellation(PromiseHelper.resolveAfter(promise, 300), this.cancellationTokenSource.token);
            this.props.onChange(result.data, result.activePage);
            this.setState({
                    activePage: result.activePage,
                    pagesCount: result.pagesCount,
                    any: result.any,
                    loading: false,
                    inited: true
                });
        });
    };

    render() {
        if (!this.props.showContent && !this.state.inited) {
            return (
                <Wrapper>
                    <Spinner type="big"/>
                </Wrapper>
            );
        }

        if (!this.props.showContent && !this.state.any) {
            return (//todo div для функтестов, выпилить когда Тихонов втащит `::local##NothingMessage`
                <div>
                    <Wrapper data-tid={"NothingMessage"}>
                        {this.props.renderNothing()}
                    </Wrapper>
                </div>
            );
        }

        return (
            <Gapped vertical gap={0}>
                {this.props.renderFilter(this.props.filterValue)}
                {this.renderContent()}
                {this.renderPaging()}
            </Gapped>
        );
    }
    
    private renderContent = () => {
        if (this.props.showContent || this.state.pagesCount) {
            return (
                <Loader active={this.state.loading} className={styles.loader}>
                    {this.props.renderContent(this.state.data)}
                </Loader>);
        }
        if (this.state.loading) {
            return (
                <Wrapper>
                    <Spinner type="normal"/>
                </Wrapper>
            );
        }
        return (
            <Wrapper data-tid={"NotFoundMessage"}>
                {this.props.renderNotFound()}
            </Wrapper>
        );
    };
    
    filter = () => {
        this.runFilter(this.props.filterValue, this.props.activePage);
    };

    private renderPaging = () => {
        return this.state.pagesCount > 1 &&
            <div className={styles.paging}>
                <Paging
                    data-tid={"Paging"}
                    activePage={this.state.activePage}
                    pagesCount={this.state.pagesCount}
                    onPageChange={this.handlePageChange}
                />
            </div>;
    };

    private filterInternal = async (value: TFilter, activePage: number, cancellation: CancellationToken): Promise<{ data: TData, activePage: number, pagesCount: number, any: boolean }> => {
        cancellation.throwIfCancellationRequested();
        
        const skip = this.props.itemsPerPage * (activePage - 1);
        const take = this.props.itemsPerPage;

        const response = await this.props.filter(value, skip, take, cancellation);
        const pagesCount = Math.ceil(response.totalCount / this.props.itemsPerPage);

        if (0 < pagesCount && pagesCount < activePage) {
            return this.filterInternal(value, pagesCount, cancellation);
        }

        return {
            data: response.data,
            activePage: activePage,
            pagesCount: pagesCount,
            any: response.any
        };
    };

    private handlePageChange = (page: number) => {
        this.setState({activePage: page}, () => this.runFilter(this.props.filterValue, this.state.activePage));
    };
}

const Wrapper = (props: { children: React.ReactNode }) => {
    const style = {
        height: 200,
        fontSize: "22px",
        color: "#a0a0a0"
    };
    return <Center style={style}>{props.children}</Center>;
};