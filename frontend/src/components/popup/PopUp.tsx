
import { Dialog, Button, Portal, CloseButton } from "@chakra-ui/react"

type PopUpProps = {
    message: string
}

function PopUp({message} : PopUpProps) { 
    <>
        <Dialog.Root>
        <Dialog.Trigger asChild>
            <Button variant="outline" size="sm">
            Open Dialog
            </Button>
        </Dialog.Trigger>
        <Portal>
            <Dialog.Backdrop />
            <Dialog.Positioner>
            <Dialog.Content>
                <Dialog.Header>
                <Dialog.Title>Dialog Title</Dialog.Title>
                </Dialog.Header>
                <Dialog.Body>
                <p>
                    {message}
                </p>
                </Dialog.Body>
                <Dialog.Footer>
                <Dialog.ActionTrigger asChild>
                    <Button variant="outline">Cancel</Button>
                </Dialog.ActionTrigger>
                <Button>Save</Button>
                </Dialog.Footer>
                <Dialog.CloseTrigger asChild>
                <CloseButton size="sm" />
                </Dialog.CloseTrigger>
            </Dialog.Content>
            </Dialog.Positioner>
        </Portal>
        </Dialog.Root>
    </>
}

export default PopUp