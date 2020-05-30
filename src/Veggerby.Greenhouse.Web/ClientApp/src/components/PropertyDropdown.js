import React from 'react';
import { DropdownButton, Dropdown } from 'react-bootstrap';

export const PropertyDropdown = ({ properties, selectProperty, selectedProperty }) => properties && properties.length ?
    (
        <DropdownButton title="Select property">
            {properties.map(property =>
                (
                    <Dropdown.Item key={property.id} onClick={() => selectProperty(property)} active={selectedProperty(property)}>
                        {property.name}
                    </Dropdown.Item>
                ))
            }
        </DropdownButton>
    )
    : 'No properties';