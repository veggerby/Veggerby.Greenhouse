import { LineChart, Line, XAxis, YAxis, Tooltip, CartesianGrid, Label, Legend, ResponsiveContainer } from 'recharts';
import React from 'react';
import moment from 'moment';

let mapMeasurements = (measurements) => {
    var result = [];

    measurements.measurements.forEach(measurement => {
        if (measurement.signalCount === 1) {
            result.push({
                time: moment(measurement.startTime).unix(),
                value: measurement.averageValue,
                annotations: measurement.annotations
            });
        }
        else {
            result.push({
                time: moment(measurement.startTime).unix(),
                value: measurement.averageValue,
                annotations: measurement.annotations
            });

            result.push({
                time: moment(measurement.endTime).unix(),
                value: measurement.averageValue,
                annotations: measurement.annotations
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

const annotationColor = '#003f5c';

let format = (value, property) => value.toLocaleString(navigator.language, { minimumFractionDigits: property.decimals, maximumFractionDigits: property.decimals }) + " " + property.unit;

const AnnotationDot = (props) => {
    const {
        cx, cy, stroke, payload, value, radius = 8
    } = props;

    if (payload.annotations && payload.annotations.length) {
        return (

        <svg
            x={cx - radius}
            y={cy - 2 * radius - 2}
            width={2 * radius}
            height={2 * radius}
            viewBox="0 0 1024 1024"
        >
            <path
                style={{ fill: "red" }}
                d="M832 64 192 64C121.6 64 64 121.6 64 192l0 512c0 70.4 57.6 128 128 128l128 0 132.096 120.448C459.072
                    957.632 466.88 960 474.432 960 493.824 960 512 944.704 512 922.496L512 832l320 0c70.4 0 128-57.6 128-128L960
                    192C960 121.6 902.4 64 832 64zM896 704c0 35.328-28.672 64-64 64L512 768c-16.96 0-33.28 6.72-45.248 18.752S448
                    815.04 448 832l0 30.08-84.864-77.376C351.296 773.952 335.936 768 320 768L192 768c-35.328 0-64-28.672-64-64L128
                    192c0-35.328 28.672-64 64-64l640 0c35.328 0 64 28.672 64 64L896 704zM736 320l-448 0C270.336 320 256 334.336 256
                    352S270.336 384 288 384l448 0C753.664 384 768 369.664 768 352S753.664 320 736 320zM736 512l-448 0C270.336
                    512 256 526.336 256 544S270.336 576 288 576l448 0C753.664 576 768 561.664 768 544S753.664 512 736 512z" />
        </svg>
        );
    }

    return null;
};

const ActiveDot = (props) => {
    const {
        cx, cy, stroke, payload, value, radius = 3
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

export const MeasurementChart = ({ measurements }) => measurements && measurements.length ?
    (
        <ResponsiveContainer width="99%" aspect={1.5}>
            <LineChart
                margin={{ top: 5, right: 20, left: 10, bottom: 5 }}
            >
                <XAxis dataKey="time" domain={['dataMin', 'dataMax']} type='number' tickFormatter={value => moment.unix(value).fromNow()}>
                    <Label
                        value="Time"
                        position="insideBottomRight"
                        dy={10}
                    />
                </XAxis>

                <YAxis dataKey="value" domain={['auto', 'auto']}>
                    <Label
                        value={measurements[0].property.name + " (" + measurements[0].property.unit + ")"}
                        unit={measurements[0].property.unit}
                        position="outside"
                        angle={-90}
                        dx={-10}
                    />
                </YAxis>

                <Tooltip
                    wrapperStyle={{
                        borderColor: 'white',
                        boxShadow: '2px 2px 3px 0px rgb(204, 204, 204)',
                    }}
                    contentStyle={{ backgroundColor: 'rgba(255, 255, 255, 0.8)' }}
                    labelStyle={{ fontWeight: 'bold', color: '#666666' }}
                    labelFormatter={v => moment.unix(v).toString() + " (" + moment.unix(v).fromNow() + ")"}
                    formatter={v => format(v, measurements[0].property)}
                />

                <CartesianGrid stroke="#f5f5f5" vertical={false} />
                {measurements.map((measurement, ixc) =>
                    <Line
                        key={measurement.sensor.key}
                        type="monotone"
                        data={mapMeasurements(measurement)}
                        dataKey="value"
                        stroke={colors[ixc % colors.length]}
                        dot={<AnnotationDot />}
                        activeDot={<ActiveDot radius={10} />}
                        name={measurement.sensor.key}
                        unit={measurements[0].property.unit}
                    />
                )}

                <Legend />
            </LineChart>
        </ResponsiveContainer>
    ) : "No measurements";
