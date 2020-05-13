import { ListGroup } from 'react-bootstrap';
import React from 'react';

export const PropertyListSmall = ({ properties, selectProperty, selectedProperty }) => properties && properties.length ?
    (
        <div style={{ paddingTop: '10px' }}>
            <h4>Properties</h4>
            <ListGroup>
                {properties.map(property =>
                    <ListGroup.Item key={property.id} action onClick={() => selectProperty(property)} active={selectedProperty(property)}>
                        <small>{property.name}</small>
                    </ListGroup.Item>
                )}
            </ListGroup>
        </div>
    )
    : 'No properties';