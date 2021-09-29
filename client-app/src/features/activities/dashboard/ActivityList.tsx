import { observer } from 'mobx-react-lite';
import React, { SyntheticEvent, useState } from 'react';
import { Button, Item, ItemContent, ItemDescription, ItemExtra, ItemGroup, ItemHeader, ItemMeta, Label, Segment } from 'semantic-ui-react';
import { useStore } from '../../../app/stores/store';

export default observer(function ActivityList() {
    const { activityStore } = useStore();
    const { deleteActivity, activitiesByDate, loading } = activityStore;
    const [target, setTarget] = useState('');

    function handleActivityDelete(e: SyntheticEvent<HTMLButtonElement>, id: string) {
        setTarget(e.currentTarget.name);
        deleteActivity(id);
    }

    return (
        <Segment>
            <ItemGroup divided>
                {activitiesByDate.map(activitie => (
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
                                    onClick={() => activityStore.selectActivity(activitie.id)}
                                    floated='right'
                                    content='View'
                                    color='blue' />
                                <Button
                                    name={activitie.id}
                                    loading={loading && target === activitie.id}
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
})