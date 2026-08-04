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
                    onChange={e => this.setState({notificationsEnabled: e.target.checked})}
                >
                    Хочу получать уведомления
                </Checkbox>
                {this.state.notificationsEnabled &&
                <Input
                    data-tid={"Input"}
                    value={this.state.email}
                    placeholder={"Адрес электронной почты"}
                    onChange={e => this.setState({email: e.target.value})}
                />}
            </Gapped>
        );
    }
}