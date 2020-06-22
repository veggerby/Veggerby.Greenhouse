import React from 'react';
import { Switch } from 'react-router-dom';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import PrivateRoute from "./components/PrivateRoute";
import './custom.css'

import appInsightsConfig from './appinsights_config.json';
import TelemetryProvider from './components/TelemetryProvider';
import { getAppInsights } from './utils/telemetry';

import { Home } from './components/Home';
import { Properties } from './components/Properties';
import { Devices } from './components/Devices';
import { Sensors } from './components/Sensors';
import { Measurements } from './components/Measurements';

import { Dashboard } from './components/Dashboard';

const App = () => {
    let appInsights = null;

    return (
        <TelemetryProvider instrumentationKey={appInsightsConfig.instrumentationKey} after={() => { appInsights = getAppInsights() }}>
            <Layout>
                <Switch>
                    <Route exact path='/' component={Home} />
                    <PrivateRoute path='/dashboard/:dashboardId' component={Dashboard} exact />
                    <PrivateRoute path='/properties' component={Properties} />
                    <PrivateRoute path='/devices' component={Devices} />
                    <PrivateRoute path='/sensors' component={Sensors} />
                    <PrivateRoute path='/measurements' component={Measurements} />
                </Switch>
            </Layout>
        </TelemetryProvider>
    );
};

export default App;