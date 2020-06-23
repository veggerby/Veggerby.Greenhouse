import { LineChart, Line, XAxis, YAxis, Tooltip, CartesianGrid, Label, Legend, ResponsiveContainer, ReferenceLine } from 'recharts';
import React from 'react';
import { Table } from 'react-bootstrap';
import moment from 'moment';
import Number from './Number';

const mapMeasurements = (measurements) => {
    var result = [];

    measurements.measurements.forEach(measurement => {
        if (measurement.signalCount <= 1) {
            result.push({
                time: moment(measurement.startTime).unix(),
                value: getValue(measurement.averageValue, measurements.property),
                annotations: measurement.annotations,
                property: measurements.property
            });
        }
        else {
            result.push({
                time: moment(measurement.startTime).unix(),
                value: getValue(measurement.averageValue, measurements.property),
                annotations: measurement.annotations,
                property: measurements.property
            });

            result.push({
                time: moment(measurement.endTime).unix(),
                value: getValue(measurement.averageValue, measurements.property),
                property: measurements.property
            });
        }
    });

    return result;
};

const getAnnotations = measurements => {
    const result = [];

    measurements.measurements.forEach(m => {
        if (m => m.annotations && m.annotations.length) {
            m.annotations.forEach(a => {
                result.push(
                    {
                        time: moment(m.startTime).unix(),
                        value: getValue(m.averageValue, measurements.property),
                        title: a.title,
                        body: a.body,
                        created: moment(a.createdUtc).unix()
                    });
            });
        }
    });

    return result;
};

const colors = [
    '#0b84a5',
    '#f6c85f',
    '#6f4e7c',
    '#9dd866',
    '#ca472f',
    '#ffa056',
    '#8dddd0'
];

const annotationColor = '#ccc';//'#003f5c';

let getValue = (value, property) => {
    if (property.domain) {
        const v = property.domain.values.find(x => x.lowerValue <= value && x.upperValue >= value);
        if (v) {
            return v.name;
        }
    }

    return value;
}

let formatValue = (value, property) => {
    if (!(value || value === 0)) {
        return "n/a";
    }

    if (property.domain) {
        return value || "n/a";
    }

    return value.toLocaleString(navigator.language, { minimumFractionDigits: property.decimals, maximumFractionDigits: property.decimals }) + " " + property.unit;
}

let formatLabel = (property) => property.name + (property.unit ? " (" + property.unit + ")" : "");

const ActiveDot = (props) => {
    const {
        cx, cy, radius = 3
    } = props;

    return (
        <svg
            x={cx - radius}
            y={cy - radius}
            width={2 * radius}
            height={2 * radius}
            viewBox="0 0 497.867 497.867"
        >
            <path
                style={{ fill: annotationColor }}
                d="M477.546,228.616h-53.567c-9.827-80.034-74.019-143.608-154.719-153.134V20.321
                    C269.259,9.096,260.155,0,248.938,0c-11.226,0-20.321,9.096-20.321,20.321v54.974c-81.375,8.941-146.257,72.808-156.15,153.313
                    H20.321C9.096,228.608,0,237.704,0,248.929s9.096,20.321,20.321,20.321H72.19c8.99,81.513,74.328,146.428,156.426,155.451v52.844
                    c0,11.226,9.096,20.321,20.321,20.321c11.217,0,20.321-9.096,20.321-20.321v-53.023c81.416-9.608,146.054-74.222,154.996-155.264
                    h53.291c11.226,0,20.321-9.096,20.321-20.321S488.771,228.616,477.546,228.616z M269.259,383.392v-67.028
                    c0-11.226-9.104-20.321-20.321-20.321c-11.226,0-20.321,9.096-20.321,20.321v67.24c-59.607-8.551-106.753-55.299-115.312-114.345
                    h68.207c11.226,0,20.321-9.096,20.321-20.321s-9.096-20.321-20.321-20.321h-67.882c9.38-58.046,56.103-103.761,114.987-112.215
                    v65.11c0,11.226,9.096,20.321,20.321,20.321c11.217,0,20.321-9.096,20.321-20.321v-64.899
                    c58.209,8.982,104.249,54.421,113.556,112.004h-66.459c-11.226,0-20.321,9.096-20.321,20.321s9.096,20.321,20.321,20.321h66.793
                    C374.646,327.842,328.191,374.297,269.259,383.392z"
            />
        </svg>
    );
};

const CustomTooltip = props => {
    const { active, payload, wrapperStyle, contentStyle } = props;

    if (!active) {
        return null;
    }

    //console.log(payload);

    return (
        <div style={wrapperStyle} className="recharts-tooltip-wrapper-top">
            <div style={contentStyle}>
                <Table size="sm">
                    <thead>
                        <tr>
                            <th>Sensor</th>
                            <th>Time</th>
                            <th>Value</th>
                        </tr>
                    </thead>
                    <tbody>
                        {payload.map(p => (
                            <tr key={p.name}>
                                <td>{p.name}</td>
                                <td>{moment.unix(p.payload.time).toString() + " (" + moment.unix(p.payload.time).fromNow() + ")"}</td>
                                <td><Number value={p.value} decimals={p.payload.property.decimals} suffix={p.payload.property.unit ? ` ${p.payload.property.unit}` : null} /></td>
                            </tr>
                        ))}
                    </tbody>
                </Table>
            </div>
        </div>)
}

const isOkData = (measurements) => measurements && measurements.length;

export const MeasurementChart = ({ measurements, measurementsSecondary }) => isOkData(measurements) ?
    (
        <ResponsiveContainer width="99%" aspect={1.5}>
            <LineChart
                margin={{ top: 5, right: 20, left: 10, bottom: 5 }}
            >
                <XAxis
                    dataKey="time"
                    domain={['dataMin', 'dataMax']}
                    type='number'
                    tickFormatter={value => moment.unix(value).fromNow()}
                >
                    <Label
                        value="Time"
                        position="insideBottomRight"
                        dy={10}
                    />
                </XAxis>

                <YAxis
                    yAxisId="primary"
                    dataKey="value"
                    domain={measurements[0].property.domain ? measurements[0].property.domain.values.map(x => x.lowerValue) : ['auto', 'auto']}
                    type={measurements[0].property.domain ? "category" : "number"}
                    tickFormatter={value => formatValue(value, measurements[0].property)}
                >
                    <Label
                        value={formatLabel(measurements[0].property)}
                        unit={measurements[0].property.unit}
                        position="outside"
                        angle={-90}
                        dx={-10}
                    />
                </YAxis>

                {isOkData(measurementsSecondary) ? (
                    <YAxis
                        yAxisId="secondary"
                        dataKey="value"
                        domain={measurementsSecondary[0].property.domain ? measurementsSecondary[0].property.domain.values.map(x => x.lowerValue) : ['auto', 'auto']}
                        orientation="right"
                        type={measurementsSecondary[0].property.domain ? "category" : "number"}
                        tickFormatter={value => formatValue(value, measurementsSecondary[0].property)}
                    >
                        <Label
                            value={formatLabel(measurementsSecondary[0].property)}
                            unit={measurementsSecondary[0].property.unit}
                            position="outside"
                            angle={-90}
                            dx={10}
                        />
                    </YAxis>
                ) : null}

                <Tooltip
                    wrapperStyle={{
                        border: '1px solid #999',
                        boxShadow: '2px 2px 3px 0px rgb(204, 204, 204)',
                    }}
                    contentStyle={{
                        padding: '10px',
                        backgroundColor: 'rgba(255, 255, 255, 0.8)'
                    }}
                    content={<CustomTooltip />}
                />

                <CartesianGrid stroke="#f5f5f5" vertical={false} />
                {measurements.map((measurement, ixc) =>
                    <Line
                        key={`${measurement.sensor.key}_${measurements[0].property.id}`}
                        type="monotone"
                        data={mapMeasurements(measurement)}
                        dataKey="value"
                        stroke={colors[ixc % colors.length]}
                        dot={false}
                        connectNulls={false}
                        //dot={<AnnotationDot />}
                        activeDot={<ActiveDot radius={10} />}
                        name={`${measurement.sensor.key}_${measurements[0].property.id}`}
                        unit={measurements[0].property.unit}
                        yAxisId="primary"
                    />
                )}

                {isOkData(measurementsSecondary) ?
                    measurementsSecondary.map((measurement, ixc) =>
                        <Line
                            key={`${measurement.sensor.key}_${measurementsSecondary[0].property.id}`}
                            type="monotone"
                            data={mapMeasurements(measurement)}
                            dataKey="value"
                            stroke={colors[(measurements.length + ixc) % colors.length]}
                            dot={false}
                            connectNulls={false}
                            //dot={<AnnotationDot />}
                            activeDot={<ActiveDot radius={10} />}
                            name={`${measurement.sensor.key}_${measurementsSecondary[0].property.id}`}
                            unit={measurementsSecondary[0].property.unit}
                            yAxisId="secondary"
                        />
                    )
                : null}

                {measurements.map(measurement => getAnnotations(measurement).map(a =>
                    <ReferenceLine x={a.time} stroke={annotationColor} yAxisId="primary">
                        <Label
                            value={a.title}
                            position="insideTop"
                            style={{ fontSize: '80%' }}
                        />
                    </ReferenceLine>))}
                <Legend />
            </LineChart>
        </ResponsiveContainer>
    ) : "No measurements";
