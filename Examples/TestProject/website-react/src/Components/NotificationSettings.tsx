import * as React from "react";
import {Gapped, Checkbox, Input} from "src/Common/Controls";

export interface UserSettingsProps {
}

export interface UserSettingsState {
    notificationsEnabled: boolean;
    email: string;
}

export default class NotificationSettings extends React.Component<UserSettingsProps, UserSettingsState> {
    state: UserSettingsState = {
        notificationsEnabled: false,
        email: "",
    };

    render() {
        return (
            <Gapped vertical>
                <Checkbox
                    data-tid={"Checkbox"}
                    checked={this.state.notificationsEnabled}
                    onChange={(e, v) => this.setState({notificationsEnabled: v})}
                >
                    Хочу получать уведомления
                </Checkbox>
                {this.state.notificationsEnabled &&
                <Input
                    data-tid={"Input"}
                    value={this.state.email}
                    placeholder={"Адрес электронной почты"}
                    onChange={(e, v) => this.setState({email: v})}
                />}
            </Gapped>
        );
    }
}