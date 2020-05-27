import React from 'react';
import { Switch } from 'react-router-dom';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Measurements } from './components/Measurements';
import PrivateRoute from "./components/PrivateRoute";
import './custom.css'

import appInsightsConfig from './appinsights_config.json';
import TelemetryProvider from './components/TelemetryProvider';
import { getAppInsights } from './utils/telemetry';

const App = () => {
    let appInsights = null;

    return (
        <TelemetryProvider instrumentationKey={appInsightsConfig.instrumentationKey} after={() => { appInsights = getAppInsights() }}>
            <Layout>
                <Switch>
                    <Route exact path='/' component={Home} />
                    <PrivateRoute path='/measurements' component={Measurements} />
                </Switch>
            </Layout>
        </TelemetryProvider>
    );
};

export default App;