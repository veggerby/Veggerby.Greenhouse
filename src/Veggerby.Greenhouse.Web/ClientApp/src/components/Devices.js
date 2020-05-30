import React, { useState, useEffect } from 'react';
import { Container, Row, Col } from 'react-bootstrap';

import * as devicesApi from '../api/devicesApi'

import { useAuth0 } from "../react-auth0-spa";
import { DeviceList } from './DeviceList';

export const Devices = () => {
    const { getTokenSilently } = useAuth0();

    const [loading, setLoading] = useState(true);

    const [devices, setDevices] = useState([]);

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

            const deviceData = await devicesApi.get(token);
            setDevices(deviceData);

            setLoading(false);
        };

        populateInitialData();
    }, [token]);

    let contents = loading ? (
        <p>
            <em>Loading...</em>
        </p>
    ) : (
            <DeviceList devices={devices} />
        );

    return (
        <Container>
            <Row>
                <Col xs={12} md={12}>
                    <h1>Devices</h1>
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
