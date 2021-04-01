import * as React from "react";
import {storiesOf} from "@storybook/react";
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
                    <Tabs.Tab id={"NotificationSettings"} href={"/iframe.html?selectedKind=Samples&selectedStory=NotificationSettings"}>Уведомления</Tabs.Tab>
                    <Tabs.Tab id={"AsyncOperation"} href={"/iframe.html?selectedKind=Samples&selectedStory=AsyncOperation"}>Операция</Tabs.Tab>
                    <Tabs.Tab id={"OrderList"} href={"/iframe.html?selectedKind=Samples&selectedStory=OrderList"}>Заказы</Tabs.Tab>
                </Tabs>
                {this.props.tab === "NotificationSettings" && <NotificationSettings/>}
                {this.props.tab === "AsyncOperation" && <AsyncOperation/>}
                {this.props.tab === "OrderList" && <OrderList/>}
            </Gapped>
        );
    }
}

storiesOf("Samples", module)
    .add("NotificationSettings", () => <App tab={"NotificationSettings"}/>)
    .add("AsyncOperation", () => <App tab={"AsyncOperation"}/>)
    .add("OrderList", () => <App tab={"OrderList"}/>);
