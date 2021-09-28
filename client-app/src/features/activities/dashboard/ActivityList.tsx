import React, { SyntheticEvent, useState } from 'react';
import { Button, Item, ItemContent, ItemDescription, ItemExtra, ItemGroup, ItemHeader, ItemMeta, Label, Segment } from 'semantic-ui-react';
import { Activity } from '../../../app/models/activity';

interface Props {
    activities: Activity[];
    selectActivity: (id: string) => void;
    deleteActivity: (id: string) => void;
    submitting: boolean;
}

export default function ActivityList({ activities, selectActivity, deleteActivity, submitting }: Props) {
    const [target, setTarget] = useState('');

    function handleActivityDelete(e: SyntheticEvent<HTMLButtonElement>, id: string) {
        setTarget(e.currentTarget.name);
        deleteActivity(id);
    }

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
                                <Button
                                    onClick={() => selectActivity(activitie.id)}
                                    floated='right'
                                    content='View'
                                    color='blue' />
                                <Button
                                    name={activitie.id}
                                    loading={submitting && target === activitie.id}
                                    onClick={(e) => handleActivityDelete(e, activitie.id)}
                                    floated='right'
                                    content='Delete'
                                    color='red' />
                                <Label basic content={activitie.category} />
                            </ItemExtra>
                        </ItemContent>
                    </Item>
                ))}
            </ItemGroup>
        </Segment>
    )
}