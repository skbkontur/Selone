import "../polyfill";
import "../react-selenium-testing-custom-props.js";
import "../react-selenium-testing";
import "../imports";
import * as React from "react";
import {addDecorator, configure} from '@storybook/react';

addDecorator(story => (
    <div className={"react-ui"} style={{padding: 20}}>
        {story()}
    </div>
));

function requireAll(requireContext) {
    return requireContext.keys().map(requireContext);
}

function loadStories() {
    requireAll(require.context("../stories", true, /\.stories\.tsx?$/));
}

configure(loadStories, module);