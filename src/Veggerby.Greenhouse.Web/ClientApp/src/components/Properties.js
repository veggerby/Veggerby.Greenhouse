import React, { useState, useEffect } from 'react';
import { Container, Row, Col } from 'react-bootstrap';

import * as propertiesApi from '../api/propertiesApi'

import { useAuth0 } from "../react-auth0-spa";

import { PropertyList } from "./PropertyList";

export const Properties = () => {
    const { getTokenSilently } = useAuth0();

    const [loading, setLoading] = useState(true);

    const [properties, setProperties] = useState([]);

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

            const propertyData = await propertiesApi.get(token);
            setProperties(propertyData);

            setLoading(false);
        };

        populateInitialData();
    }, [token]);

    let contents = loading ? (
        <p>
            <em>Loading...</em>
        </p>
    ) : (
            <PropertyList properties={properties} />
        );

    return (
        <Container>
            <Row>
                <Col xs={12} md={12}>
                    <h1>Properties</h1>
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
