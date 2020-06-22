import React, { useState, useEffect } from 'react';
import { Button, Container, Col, Row } from 'react-bootstrap';
import { useCookies } from 'react-cookie';
import { MeasurementChart } from './MeasurementChart';
import { useAuth0 } from '../react-auth0-spa';

import * as measurementsApi from '../api/measurementsApi';
import { useParams } from 'react-router-dom';

import dashboards from "../dashboards.json";

export const Dashboard = () => {
    const { dashboardId } = useParams();
    const { isAuthenticated, getTokenSilently } = useAuth0();
    const [loading, setLoading] = useState(true);
    const [refresh, setRefresh] = useState(null);
    const [dashboard, setDashboard] = useState(null);
    const [measurements, setMeasurements] = useState([]);

    const [token, setToken] = useState(null);

    useEffect(() => {
        const new_dashboard = dashboards.find(d => d.route === dashboardId);
        setDashboard(new_dashboard);
        setMeasurements([]);
    }, [dashboardId]);

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
        const populateMeasurementsData = async (chart, set) => {
            const measurementsData = await measurementsApi.get(token, chart.sensors.map(s => ({ key: s })), { id: chart.property }, chart.days || 1.5);
            set(measurementsData);
        };

        if (!dashboard) {
            setMeasurements([]);
            return;
        }

        const full_result = [];

        setLoading(true);

        dashboard.charts.map(chart => {
            let result = { title: chart.title, primary: null, secondary: null };
            full_result.push(result);

            populateMeasurementsData(chart.primary, data => result.primary = data);

            if (chart.secondary) {
                populateMeasurementsData(chart.secondary, data => result.secondary = data);
            }
        });

        setLoading(false);

        setMeasurements(full_result);

        // eslint-disable-next-line
    }, [token, dashboard, refresh]);

    return dashboard ? (
        <Container>
            <Row className="justify-content-md-left justify-content-lg-left">
                <h1>{dashboard.title}</h1>
                <Button onClick={() => setRefresh(new Date())}>Refresh</Button>
            </Row>
            {measurements.length ? measurements.map(measurement =>
                <>
                    <Row>
                        <Col>
                            <h2>{measurement.title}</h2>
                            {measurement.primary ? <MeasurementChart measurements={measurement.primary} measurementsSecondary={measurement.secondary} /> : null}
                        </Col>
                    </Row>
                </>
            ) : null}

        </Container>
    ) : <i>No dashboard found with id {dashboardId}</i>;
};