import React from 'react'
import { Menu, Container, Button } from 'semantic-ui-react'

interface IProps {
    openCreateForm: () => void;
}

export const Navbar: React.FC<IProps> = ({ openCreateForm }) => {
    return (
        <Menu fixed='top' inverted>
            <Container>
                <Menu.Item header>
                    <img src="/assets/logo.png" style={{ marginRight: '10px' }} alt="" />
                    Reactivities
                </Menu.Item>
                <Menu.Item name='Activities' />
                <Menu.Item >
                    <Button positive content='Create Activity' onClick={openCreateForm} />
                </Menu.Item>
            </Container>
        </Menu>
    )
}
