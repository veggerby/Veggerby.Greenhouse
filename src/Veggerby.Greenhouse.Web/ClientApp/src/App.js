import React from 'react';
import { Switch } from 'react-router-dom';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Measurements } from './components/Measurements';
import PrivateRoute from "./components/PrivateRoute";
import './custom.css'

const App = () => {
    return (
        <Layout>
            <Switch>
                <Route exact path='/' component={Home} />
                <PrivateRoute path='/measurements' component={Measurements} />
            </Switch>
        </Layout>
    );
};

export default App;