import React from 'react';
import { Button, Item, ItemContent, ItemDescription, ItemExtra, ItemGroup, ItemHeader, ItemMeta, Label, Segment } from 'semantic-ui-react';
import { Activity } from '../../../app/models/activity';

interface Props {
    activities: Activity[];
    selectActivity: (id: string) => void;
    deleteActivity: (id: string) => void;
}

export default function ActivityList({ activities, selectActivity, deleteActivity }: Props) {
    return (
        <Segment>
            <ItemGroup divided>
                {activities.map(activitie => (
                    <Item key={activitie.id}>
                        <ItemContent>
                            <ItemHeader as='a'>{activitie.title}</ItemHeader>
                            <ItemMeta>{activitie.date}</ItemMeta>
                            <ItemDescription>
                                <div>{activitie.description}</div>
                                <div>{activitie.city}, {activitie.venue}</div>
                            </ItemDescription>
                            <ItemExtra>
                                <Button onClick={() => selectActivity(activitie.id)} floated='right' content='View' color='blue' />
                                <Button onClick={() => deleteActivity(activitie.id)} floated='right' content='Delete' color='red' />
                                <Label basic content={activitie.category} />
                            </ItemExtra>
                        </ItemContent>
                    </Item>
                ))}
            </ItemGroup>
        </Segment>
    )
}