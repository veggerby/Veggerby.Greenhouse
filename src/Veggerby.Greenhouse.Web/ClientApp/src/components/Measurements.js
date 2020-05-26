import React, { useState, useEffect } from 'react';
import { Container, Row, Col, Tabs, Tab } from 'react-bootstrap';

import { DeviceList } from './DeviceList';
import { PropertyList } from './PropertyList'
import { MeasurementChart } from './MeasurementChart';
import { MeasurementTable } from './MeasurementTable';
import { PropertyListSmall } from './PropertyListSmall';
import { SensorListSmall } from './SensorListSmall';
import { SensorList } from './SensorList';

import * as sensorsApi from '../api/sensorsApi'
import * as devicesApi from '../api/devicesApi'
import * as propertiesApi from '../api/propertiesApi'
import * as measurementsApi from '../api/measurementsApi'

import { useAuth0 } from "../react-auth0-spa";

export const Measurements = () => {
    const { getTokenSilently } = useAuth0();

    const [loading, setLoading] = useState(true);
    const [loadingData, setLoadingData] = useState(false);

    const [activeTab, setActiveTab] = useState('chart');

    const [properties, setProperties] = useState([]);
    const [devices, setDevices] = useState([]);
    const [sensors, setSensors] = useState([]);
    const [measurements, setMeasurements] = useState([]);

    const [selectedProperty, selectProperty] = useState(null);
    const [selectedSensors, setSelectedSensors] = useState([]);
    const [selectedDevices, setSelectedDevices] = useState([]);

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
                setSelectedSensors(sensorData);
            }

            const deviceData = await devicesApi.get(token);
            setDevices(deviceData);

            const propertiesData = await propertiesApi.get(token);
            setProperties(propertiesData);

            if (propertiesData && propertiesData.length) {
                selectProperty(propertiesData[0]);
            }

            setLoading(false);
        };

        populateInitialData();
    }, [token]);

    useEffect(() => {
        const populateMeasurementsData = async () => {
            if (!(token && selectedProperty && selectedSensors && selectedSensors.length)) {
                return;
            }

            setLoadingData(true);

            const measurementsData = await measurementsApi.get(token, selectedSensors, selectedProperty);
            setMeasurements(measurementsData);

            setLoadingData(false);
        };

        populateMeasurementsData();
    }, [token, selectedSensors, selectedProperty]);

    const isSelected = (list, item) => {
        return list.indexOf(item) !== -1;
    }

    const toggleList = (list, item) => {
        var array = [...list];
        if (isSelected(array, item)) {
            array = array.filter(x => x != item);
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

    const toggleDevice = d => {
        var list = toggleList(selectedDevices, d);
        setSelectedDevices(list);
    }

    const renderData = (properties, devices, sensors, measurements) => {
        return (
            <Tabs id="measurementsTab" activeKey={activeTab} onSelect={setActiveTab} variant="pills">
                <Tab eventKey="chart" title="Chart">
                    {!loadingData ? <MeasurementChart measurements={measurements} /> : "Loading..."}
                    <PropertyListSmall properties={properties} selectProperty={selectProperty} selectedProperty={p => isSelected([selectedProperty], p)} />
                    <SensorListSmall sensors={sensors} selectSensor={toggleSensor} selectedSensor={s => isSelected(selectedSensors, s)} />
                </Tab>
                <Tab eventKey="properties" title="Properties">
                    <PropertyList properties={properties} selectProperty={selectProperty} selectedProperty={p => isSelected([selectedProperty], p)} />
                </Tab>
                <Tab eventKey="sensors" title="Sensors">
                    <SensorList sensors={sensors} selectSensor={toggleSensor} selectedSensor={s => isSelected(selectedSensors, s)} />
                </Tab>
                <Tab eventKey="devices" title="Devices">
                    <DeviceList devices={devices} selectDevice={toggleDevice} selectedDevice={d => isSelected(selectedDevices, d)} />
                </Tab>
                <Tab eventKey="table" title="Table">
                {!loadingData ? <MeasurementTable measurements={measurements} /> : "Loading"}
                </Tab>
            </Tabs>
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
