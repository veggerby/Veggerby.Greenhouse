import React, { useState, useEffect } from 'react';
import { Button, Container, Col, Row } from 'react-bootstrap';
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
        if (!token) {
            return;
        }

        const new_dashboard = dashboards.find(d => d.route === dashboardId);
        setDashboard(new_dashboard);
    }, [token, dashboardId]);

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
        if (!token) {
            return;
        }

        const populateMeasurementsData = async (chart, days) => {
            const measurementsData = await measurementsApi.get(token, chart.sensors.map(s => ({ key: s })), { id: chart.property }, days || 1.5);
            return measurementsData;
        };

        const getChartData = async chart => {
            const result = {
                title: chart.title,
                primary: null,
                secondary: null
            };

            result.primary = await populateMeasurementsData(chart.primary, chart.days);

            if (chart.secondary) {
                result.secondary = await populateMeasurementsData(chart.secondary, chart.days);
            }

            return result;
        };

        if (!dashboard) {
            setMeasurements([]);
            return;
        }

        setLoading(true);

        Promise.all(dashboard.charts.map(getChartData)).then(responses => {
            setMeasurements(responses);
            setLoading(false);
        });
    }, [token, dashboard, refresh]);

    return dashboard ? (
        <Container>
            <Row className="justify-content-md-left justify-content-lg-left">
                <h1>{dashboard.title}</h1>
                <Button onClick={() => setRefresh(new Date())}>Refresh</Button>
            </Row>
            {loading ? (<i>Loading....</i>) :
                (
                    measurements.length ? measurements.map((measurement, ixc) =>
                        <Row key={ixc}>
                            <Col>
                                <h2>{measurement.title}</h2>
                                {measurement.primary ? <MeasurementChart measurements={measurement.primary} measurementsSecondary={measurement.secondary} /> : null}
                            </Col>
                        </Row>
                    ) : null
                )
            }

        </Container>
    ) : <i>No dashboard found with id {dashboardId}</i>;
};