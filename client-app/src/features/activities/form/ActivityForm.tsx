import React, { useState, FormEvent } from 'react'
import { Segment, Form, Button } from 'semantic-ui-react'
import { IActivity } from '../../../app/models/activity'
import { v4 as uuid } from 'uuid';

interface IProp {
    setEditMode: (editMode: boolean) => void;
    activity: IActivity;
    createActivity: (activity: IActivity) => void;
    editActivity: (activity: IActivity) => void;
    submitting: boolean
}

export const ActivityForm: React.FC<IProp> = ({
    setEditMode,
    activity: initialFormState,
    createActivity,
    editActivity,
    submitting
}) => {

    const initialiseForm = () => {
        if (initialFormState) {
            return initialFormState;
        } else {
            return {
                id: '',
                title: '',
                category: '',
                description: '',
                date: '',
                city: '',
                venue: ''
            };
        }
    };

    const [activity, setActivity] = useState<IActivity>(initialiseForm);

    const handleImputChange = (event: FormEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        const { name, value } = event.currentTarget;
        setActivity({ ...activity, [name]: value })
    }

    const handleSubmit = () => {
        if (activity.id.length === 0) {
            let newActivity = {
                ...activity,
                id: uuid()
            }
            createActivity(newActivity);
        } else {
            editActivity(activity)
        }
    }

    return (
        <Segment clearing>
            <Form onSubmit={handleSubmit}>
                <Form.Input
                    placeholder='Title'
                    value={activity.title}
                    onChange={handleImputChange}
                    name='title'
                />
                <Form.TextArea
                    rows={2}
                    placeholder='Description'
                    value={activity.description}
                    onChange={handleImputChange}
                    name='description'
                />
                <Form.Input
                    placeholder='Category'
                    value={activity.category}
                    onChange={handleImputChange}
                    name='category'
                />
                <Form.Input
                    onChange={handleImputChange}
                    name='date'
                    type='datetime-local'
                    placeholder='Date'
                    value={activity.date}
                />
                <Form.Input
                    placeholder='City'
                    value={activity.city}
                    onChange={handleImputChange}
                    name='city'
                />
                <Form.Input
                    placeholder='Venue'
                    value={activity.venue}
                    onChange={handleImputChange}
                    name='venue'
                />
                <Button loading={submitting} floated='right' positive type='submit' content='Submit' />
                <Button floated='right' type='button' content='Cancel'
                    onClick={() => setEditMode(false)} />
            </Form>
        </Segment>
    )
}
