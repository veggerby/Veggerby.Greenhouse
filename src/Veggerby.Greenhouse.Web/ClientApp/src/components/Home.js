import React, { useState, useEffect } from 'react';
import { Button, Container, Col, Row } from 'react-bootstrap';
import { useCookies } from 'react-cookie';
import { MeasurementChart } from './MeasurementChart';
import { useAuth0 } from '../react-auth0-spa';

import * as measurementsApi from '../api/measurementsApi';

const COOKIE_NAME = 'auth0.is.authenticated';

export const Home = () => {
    const { isAuthenticated, getTokenSilently, loginWithRedirect } = useAuth0();
    const [loading, setLoading] = useState(true);
    const [refresh, setRefresh] = useState(null);
    const [measurementsTemp, setMeasurementsTemp] = useState([]);
    const [measurementsHumidity, setMeasurementsHumidity] = useState([]);

    // eslint-disable-next-line
    const [cookies, setCookie, removeCookie] = useCookies(['COOKIE_NAME']);

    const [token, setToken] = useState(null);

    useEffect(() => {
        const getToken = async () => {

            setLoading(true);

            const token = await getTokenSilently();
            setToken(token);

            setLoading(false);
        }

        if (isAuthenticated) {
            getToken();
        }
        // eslint-disable-next-line
    }, []);

    useEffect(() => {
        const populateMeasurementsData = async (property, set) => {
            setLoading(true);

        const measurementsData = await measurementsApi.get(token, 'dht22@pi-zero', { id: property }, 1.5);
            set(measurementsData);

            setLoading(false);
        };

        populateMeasurementsData('temperature', setMeasurementsTemp);
        populateMeasurementsData('humidity', setMeasurementsHumidity);

        // eslint-disable-next-line
    }, [token, refresh]);

    return (
        <Container>
            <Row className="justify-content-md-left justify-content-lg-left">
                <h1>Veggerby Greenhouse</h1>
            </Row>
            {isAuthenticated ? (
                <Row>
                    <Col>{measurementsTemp ? <MeasurementChart measurements={measurementsTemp} measurementsSecondary={measurementsHumidity} /> : null}</Col>
                </Row>
            ) : null}



            <Button onClick={() => { removeCookie(COOKIE_NAME); window.location.reload(true); }}>Remove Auth & Reload</Button>
            &nbsp;
            <Button onClick={() => { removeCookie(COOKIE_NAME); loginWithRedirect({}); }}>Remove Auth & Login</Button>
            &nbsp;
            <Button onClick={() => setRefresh(new Date())}>Refresh</Button>
        </Container>
    );
};