import React, { useState, useEffect } from 'react';
import { Container, Row, Col } from 'react-bootstrap';

import * as sensorsApi from '../api/sensorsApi'

import { useAuth0 } from "../react-auth0-spa";
import { SensorList } from './SensorList';

export const Sensors = () => {
    const { getTokenSilently } = useAuth0();

    const [loading, setLoading] = useState(true);

    const [sensors, setSensors] = useState([]);

    const [token, setToken] = useState(null);

    useEffect(() => {
        const getToken = async () => {
            setLoading(true);

            const token = await getTokenSilently();
            setToken(token);

            setLoading(false);
        }

        getToken();
        // eslint-disable-next-line
    }, []);

    useEffect(() => {
        const populateInitialData = async () => {
            if (!token) {
                return;
            }

            setLoading(true);

            const sensorData = await sensorsApi.get(token);
            setSensors(sensorData);

            setLoading(false);
        };

        populateInitialData();
    }, [token]);

    let contents = loading ? (
        <p>
            <em>Loading...</em>
        </p>
    ) : (
            <SensorList sensors={sensors} />
        );

    return (
        <Container>
            <Row>
                <Col xs={12} md={12}>
                    <h1>Sensors</h1>
                </Col>
            </Row>
            <Row>
                <Col xs={12} md={12}>
                    {contents}
                </Col>
            </Row>
        </Container>
    );
};
