import * as React from "react";
import {Button, Checkbox, Gapped, Input, Link, Modal} from "src/Common/Controls";
import PromiseHelper from "src/Common/TypeHelpers/PromiseHelper";
import {default as ContentWithFilterAndPaging, FilterResult} from "../Common/Controls/ContentWithFilterAndPaging/ContentWithFilterAndPaging";
import Table from "../Common/Controls/Table/Table";
import CurrencyLabel from "../Common/Controls/CurrencyLabel/CurrencyLabel";
import {Refresh, Trash} from "@skbkontur/react-icons";

export interface OrderListProps {
}

export interface OrderListState {
    filter: OrderListFilterValue;
    data: OrderListData;
    activePage: number;
    viewId: string;
    reloading: string[];
}

export interface OrderListFilterValue {
    search: string;
}

export interface OrderListData {
    items: OrderListItem[];
}

interface OrderListItem {
    id: string;
    fio: string;
    phone: string;
    sum: number;
    verified: boolean;
}

const template: OrderListItem[] = [
    {id: "xxx01", fio: "Савельев Вячеслав", phone: "+7-998-234-23-23", sum: 434, verified: true},
    {id: "xxx02", fio: "Назаров Иван", phone: "+7-999-111-11-11", sum: 150, verified: false},
    {id: "xxx03", fio: "Титов Кондратий", phone: "+7-444-111-00-00", sum: 2, verified: true},
    {id: "xxx04", fio: "Ширяев Август", phone: "+7-929-572-47-24", sum: 945.2, verified: true},
    {id: "xxx05", fio: "Куликов Герасим", phone: "+7-924-572-67-03", sum: 98, verified: false},
    {id: "xxx06", fio: "Моисеев Иван", phone: "+7-999-222-22-22", sum: 160.5, verified: false},
    {id: "xxx07", fio: "Беляев Владислав", phone: "+7-935-636-20-53", sum: 61, verified: true},
    {id: "xxx08", fio: "Галкин Исаак", phone: "+7-998-065-91-52", sum: 23, verified: true},
    {id: "xxx09", fio: "Кириллов Мстислав", phone: "+7-952-952-52-52", sum: 35, verified: false},
    {id: "xxx10", fio: "Колобов Власий", phone: "+7-998-733-72-72", sum: 434, verified: true},
    {id: "xxx11", fio: "Карпов Иван", phone: "+7-999-333-33-33", sum: 170.55, verified: true},
    {id: "xxx12", fio: "Аксёнов Ираклий", phone: "+7-919-453-70-64", sum: 2, verified: false},
    {id: "xxx13", fio: "Титов Платон", phone: "+7-444-222-00-00", sum: 51, verified: false},
];

let removed: string[] = [];

let source: OrderListItem[] = template;

interface OrderListFilterProps {
    value: OrderListFilterValue;
    onChange: (value: OrderListFilterValue) => void;
}

class OrderListFilter extends React.Component<OrderListFilterProps> {
    render() {
        return (
            <div>
                <Gapped>
                    <Input
                        data-tid={"SearchInput"}
                        placeholder={"Начните вводить ФИО"}
                        value={this.props.value.search}
                        onChange={(e, v) => this.props.onChange({search: v})}
                    />
                    <Button
                        data-tid={"ResetButton"}
                        onClick={() => this.props.onChange({search: ""})}
                    >
                        Сбросить
                    </Button>
                    <div style={{width: 2, height: "100%", background: "lightgrey"}}/>
                    <Button
                        data-tid={"SearchIvanButton"}
                        onClick={() => this.props.onChange({search: "Иван"})}
                    >
                        Найти Ивана
                    </Button>
                    <Button
                        data-tid={"SearchTitovButton"}
                        onClick={() => this.props.onChange({search: "Титов"})}
                    >
                        Найти Титова
                    </Button>
                    <Button
                        data-tid={"SearchNothingButton"}
                        onClick={() => this.props.onChange({search: "---"})}
                    >
                        Найти ничего
                    </Button>
                </Gapped>
            </div>
        );
    }
}

interface OrderListTableProps {
    reloading: string[];
    value: OrderListData;
    onClick: (id: string) => void;
    onReload: (id: string) => void;
    onRemove: (id: string) => void;
}

class OrderListTable extends React.Component<OrderListTableProps> {
    render() {
        return (
            <Table>
                <Table.Header>
                    <Table.Th width={70}>Проверен</Table.Th>
                    <Table.Th width={60}>Заказ №</Table.Th>
                    <Table.Th width={"100%"}>ФИО</Table.Th>
                    <Table.Th width={140}>Телефон</Table.Th>
                    <Table.Th width={60} align={"right"}>Сумма</Table.Th>
                    <Table.Th width={16}>{""}</Table.Th>
                    <Table.Th width={16}>{""}</Table.Th>
                </Table.Header>
                <Table.Body>
                    {this.props.value.items.filter(x => !this.props.reloading.includes(x.id)).map(x => {
                        return (
                            <Table.Tr key={x.id} data-tid={"Order"} onClick={() => this.props.onClick(x.id)}>
                                <Table.Td><Checkbox data-tid={"Verified"} checked={x.verified} disabled/></Table.Td>
                                <Table.Td data-tid={"Id"}>{x.id}</Table.Td>
                                <Table.Td data-tid={"Fio"}>{x.fio}</Table.Td>
                                <Table.Td data-tid={"Phone"}>{x.phone}</Table.Td>
                                <Table.Td data-tid={"Sum"} align={"right"}><CurrencyLabel value={x.sum}/></Table.Td>
                                <Table.Td>
                                    <Link data-key={"ssdfsdf"}
                                          data-tid={"ReloadLink"}
                                          icon={<Refresh/>}
                                          onClick={e => {
                                              e.stopPropagation();
                                              this.props.onReload(x.id);
                                          }}
                                    />
                                </Table.Td>
                                <Table.Td>
                                    <Link
                                        data-tid={"RemoveLink"}
                                        icon={<Trash/>}
                                        onClick={e => {
                                            e.stopPropagation();
                                            this.props.onRemove(x.id);
                                        }}
                                    />
                                </Table.Td>
                            </Table.Tr>
                        );
                    })}
                </Table.Body>
            </Table>
        );
    }
}

export default class OrderList extends React.Component<OrderListProps, OrderListState> {
    state: OrderListState = {
        activePage: 1,
        data: null,
        filter: {
            search: "",
        },
        viewId: null,
        reloading: [],
    };

    component: ContentWithFilterAndPaging<OrderListFilterValue, OrderListData>;

    render() {
        return (
            <ContentWithFilterAndPaging
                ref={e => this.component = e as any}
                filter={this.filter}
                itemsPerPage={5}
                activePage={this.state.activePage}
                filterValue={this.state.filter}
                renderFilter={this.renderFilter}
                renderContent={this.renderContent}
                renderNothing={this.renderNothing}
                renderNotFound={this.renderNotFound}
                onChange={(data, activePage) => this.setState({data: data as OrderListData, activePage})}
            />
        );
    }

    private filter = async (filter: OrderListFilterValue, skip: number, take: number): Promise<FilterResult<OrderListData>> => {
        const exists = source.filter(x => !removed.includes(x.id));
        const accepted = exists.filter(x => x.fio.toLowerCase().includes(filter.search.toLowerCase()));
        const filtered = accepted.slice(skip, skip + take);
        await PromiseHelper.delay(1000 + 2000 * Math.random());
        return ({
            any: true,
            data: {items: filtered},
            takeCount: null,
            totalCount: accepted.length,
        });
    };

    private renderFilter = () => {
        return (
            <div>
                <OrderListFilter
                    data-tid={"Filter"}
                    value={this.state.filter}
                    onChange={v => this.setState({filter: v})}
                />
            </div>
        );
    };

    private renderContent = () => {
        return (
            <div data-tid={"Results"} style={{minHeight: 150, width: 750}}>
                <OrderListTable
                    reloading={this.state.reloading}
                    value={this.state.data}
                    onClick={this.handleClick}
                    onReload={this.handleReload}
                    onRemove={this.handleRemove}
                />
                {this.state.viewId && this.renderModal(this.state.viewId)}
            </div>
        );
    };

    private renderModal = (id: string) => {
        const order = this.state.data.items.filter(x => x.id === id)[0];
        return (
            <Modal onClose={() => this.setState({viewId: null})} ignoreBackgroundClick={false}>
                <Modal.Body>
                    <div data-tid={"OrderModal"} style={{width: 300, fontSize: 16}}>
                        <Gapped vertical gap={15}>
                            <b>{order.id}</b>
                            <span>{order.verified ? "Проверен" : "Ожидает проверки"}</span>
                            <span>{order.fio}</span>
                            <span>{order.phone}</span>
                            <CurrencyLabel data-tid={"Sum"} value={order.sum}/>
                            <Button
                                data-tid={"CloseButton"}
                                onClick={() => this.setState({viewId: null})}
                            >
                                Закрыть
                            </Button>
                        </Gapped>
                    </div>
                </Modal.Body>
            </Modal>
        );
    };


    private renderNothing = () => {
        return (
            <div>
                Nothing
            </div>
        );
    };

    private renderNotFound = () => {
        return (
            <div data-tid={"NotFoundMessage"}>
                Ничего не нашлось
            </div>
        );
    };

    private handleClick = (id: string) => {
        this.setState({viewId: id});
    };

    private handleReload = (id: string) => {
        this.setState(state => ({
            reloading: [...state.reloading, id],
        }), async () => {
            await PromiseHelper.delay(200);
            this.setState({
                reloading: this.state.reloading.filter(x => x !== id),
                data: {
                    items: this.state.data.items.map(x => x.id !== id ? x : ({...x, sum: x.sum + 1})),
                },
            });
        });
    };

    private handleRemove = (id: string) => {
        removed.push(id);
        this.setState(state => ({
            data: {
                items: state.data.items.filter(x => x.id != id),
            },
        }), () => {
            if (this.state.data.items.length === 0) {
                this.component.filter();
            }
        });
    };
}