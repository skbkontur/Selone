import * as React from "react";
import type {Meta, StoryObj} from "@storybook/react";
import NotificationSettings from "../src/Components/NotificationSettings";
import AsyncOperation from "../src/Components/AsyncOperation";
import OrderList from "../src/Components/OrderList";
import {Gapped, Tabs} from "../src/Common/Controls";

type AppTab = "NotificationSettings" | "AsyncOperation" | "OrderList";

interface AppProps {
    tab: AppTab;
}

class App extends React.Component<AppProps> {
    render() {
        return (
            <Gapped vertical>
                <Tabs value={this.props.tab}>
                    <Tabs.Tab id={"NotificationSettings"} href={"/iframe.html?id=samples--notification-settings-story&viewMode=story"}>Уведомления</Tabs.Tab>
                    <Tabs.Tab id={"AsyncOperation"} href={"/iframe.html?id=samples--async-operation-story&viewMode=story"}>Операция</Tabs.Tab>
                    <Tabs.Tab id={"OrderList"} href={"/iframe.html?id=samples--order-list-story&viewMode=story"}>Заказы</Tabs.Tab>
                </Tabs>
                {this.props.tab === "NotificationSettings" && <NotificationSettings/>}
                {this.props.tab === "AsyncOperation" && <AsyncOperation/>}
                {this.props.tab === "OrderList" && <OrderList/>}
            </Gapped>
        );
    }
}

const meta: Meta = {title: "Samples"};
export default meta;

export const NotificationSettingsStory: StoryObj = {name: "NotificationSettings", render: () => <App tab={"NotificationSettings"}/>};
export const AsyncOperationStory: StoryObj = {name: "AsyncOperation", render: () => <App tab={"AsyncOperation"}/>};
export const OrderListStory: StoryObj = {name: "OrderList", render: () => <App tab={"OrderList"}/>};
