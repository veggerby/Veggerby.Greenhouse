import React, { useState, useEffect } from 'react';
import { useCookies } from 'react-cookie';
import { Container, Row, Col, Tabs, Tab, Dropdown, DropdownButton, Button } from 'react-bootstrap';

import { MeasurementChart } from './MeasurementChart';
import { MeasurementTable } from './MeasurementTable';

import * as sensorsApi from '../api/sensorsApi';
import * as devicesApi from '../api/devicesApi';
import * as propertiesApi from '../api/propertiesApi';
import * as measurementsApi from '../api/measurementsApi';

import { useAuth0 } from "../react-auth0-spa";
import { PropertyDropdown } from './PropertyDropdown';
import { SensorDropdown } from './SensorDropdown';

const COOKIE_NAME = 'greenhouseMeasurements';

export const Measurements = () => {
    const [cookies, setCookie] = useCookies([COOKIE_NAME]);

    const { getTokenSilently } = useAuth0();

    const [loading, setLoading] = useState(true);
    const [loadingData, setLoadingData] = useState(false);
    const [refresh, setRefresh] = useState(null);

    const [activeTab, setActiveTab] = useState('chart');

    const [properties, setProperties] = useState([]);
    const [devices, setDevices] = useState([]);
    const [sensors, setSensors] = useState([]);
    const [measurements, setMeasurements] = useState([]);

    const [selectedProperty, selectProperty] = useState(null);
    const [selectedSensors, setSelectedSensors] = useState([]);
    const [timeframe, setTimeframe] = useState(1);

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
            if (sensorData && sensorData.length) {
                if (cookies.greenhouseMeasurements) {
                    setSelectedSensors(sensorData.filter(x => x.enabled && x.device.enabled && cookies.greenhouseMeasurements.selectedSensors.indexOf(x.key) !== -1));
                }
                else {
                    setSelectedSensors(sensorData.filter(x => x.enabled && x.device.enabled));
                }
            }

            const deviceData = await devicesApi.get(token);
            setDevices(deviceData);

            const propertiesData = await propertiesApi.get(token);
            setProperties(propertiesData);

            if (propertiesData && propertiesData.length) {
                if (cookies.greenhouseMeasurements) {
                    selectProperty(propertiesData.find(x => x.id === cookies.greenhouseMeasurements.selectedProperty));
                }
                else {
                    selectProperty(propertiesData[0]);
                }
            }

            if (cookies.greenhouseMeasurements) {
                setTimeframe(cookies.greenhouseMeasurements.timeframe);
            }

            setLoading(false);
        };

        populateInitialData();
        // eslint-disable-next-line
    }, [token]);

    useEffect(() => {
        const populateMeasurementsData = async () => {
            if (!(token && selectedProperty && selectedSensors && selectedSensors.length)) {
                return;
            }

            setLoadingData(true);

            const measurementsData = await measurementsApi.get(token, selectedSensors, selectedProperty, timeframe);
            setMeasurements(measurementsData);

            setCookie(COOKIE_NAME, { selectedProperty: selectedProperty.id, selectedSensors: selectedSensors.map(x => x.key), timeframe: timeframe})

            setLoadingData(false);
        };

        populateMeasurementsData();
        // eslint-disable-next-line
    }, [token, selectedSensors, selectedProperty, timeframe, refresh]);

    const isSelected = (list, item) => {
        return list.indexOf(item) !== -1;
    }

    const toggleList = (list, item) => {
        var array = [...list];
        if (isSelected(array, item)) {
            array = array.filter(x => x !== item);
        }
        else {
            array.push(item);
        }

        return array;
    }

    const toggleSensor = s => {
        var list = toggleList(selectedSensors, s);
        setSelectedSensors(list);
    }

    const sensorsTitle = () => !(measurements && measurements.length > 0) ? "no sensors" :
            (measurements.length <= 3 ?
                measurements.map(m => m.sensor.key)/*`${m.sensor.name} on ${m.sensor.device.name}`)*/.join(', ') :
                `${measurements.length} sensors`);

    const sensorsTitleSelected = () => !(selectedSensors && selectedSensors.length > 0) ? "no sensors" :
        (selectedSensors.length <= 3 ?
            selectedSensors.map(m => m.key)/*`${m.sensor.name} on ${m.sensor.device.name}`)*/.join(', ') :
            `${selectedSensors.length} sensors`);

    const renderData = (properties, devices, sensors, measurements) => {
        return (
            <Container>
                <Row className="justify-content-md-left justify-content-lg-left">
                    <Col xs md="auto" lg="auto">
                        <PropertyDropdown properties={properties} selectProperty={selectProperty} selectedProperty={p => isSelected([selectedProperty], p)} />
                    </Col>
                    <Col xs md="auto" lg="auto">
                        <SensorDropdown sensors={sensors} selectSensor={toggleSensor} selectedSensor={s => isSelected(selectedSensors, s)} />
                    </Col>
                    <Col xs md="auto" lg="auto">
                        <DropdownButton title="Select time frame">
                            {[1, 2, 3, 4, 5].map(days =>
                                (
                                    <Dropdown.Item key={days} onClick={() => setTimeframe(days)} active={days === timeframe}>
                                        {days} day{days > 1 ? "s" : ""}
                                    </Dropdown.Item>
                                ))
                            }
                        </DropdownButton>
                    </Col>
                    <Col xs md="auto" lg="auto">
                        <Button onClick={() => setRefresh(new Date())}>Refresh</Button>
                    </Col>
                </Row>
                <Row style={{ paddingTop: '10px' }}>
                    <Col>
                        {!loadingData ?
                            <>
                                <h3>{selectedProperty.name} for {sensorsTitle()}</h3>
                                <Tabs id="measurementsTab" activeKey={activeTab} onSelect={setActiveTab} variant="pills">
                                    <Tab eventKey="chart" title="Chart">
                                        <MeasurementChart measurements={measurements} />
                                    </Tab>
                                    <Tab eventKey="table" title="Table">
                                        <MeasurementTable measurements={measurements} />
                                    </Tab>
                                </Tabs>
                                </>
                            : `Loading ${selectedProperty.name} for ${sensorsTitleSelected()}...`
                        }
                    </Col>
                </Row>
            </Container>
        );
    }

    let contents = loading ? (
        <p>
            <em>Loading...</em>
        </p>
    ) : (
            renderData(properties, devices, sensors, measurements)
        );

    return (
        <Container>
            <Row>
                <Col xs={12} md={12}>
                    <h1>Measurements</h1>
                    <p>Select a property and a device to show measurements</p>
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
