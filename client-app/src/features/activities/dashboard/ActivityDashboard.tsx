import React, { useEffect } from 'react';
import { Grid } from 'semantic-ui-react';
import ActivityList from './ActivityList';
import { useStore } from '../../../app/stores/store';
import { observer } from 'mobx-react-lite';
import LoadingComponent from '../../../app/layout/LoadingComponent';
import ActivityFilters from './ActivityFilters';

export default observer(function ActivityDashboard() {
    const { activityStore } = useStore();
    const { activityRegistry, loadActivitites } = activityStore;

    useEffect(() => {
        if (activityRegistry.size <= 1) { loadActivitites(); }
    }, [activityRegistry.size, loadActivitites]);

    if (activityStore.loadingInitial) return <LoadingComponent content='Loading activitites...' />

    return (
        <Grid>
            <Grid.Column width='10'>
                <ActivityList />
            </Grid.Column>
            <Grid.Column width='6'>
                <ActivityFilters />
            </Grid.Column>
        </Grid>
    )
})