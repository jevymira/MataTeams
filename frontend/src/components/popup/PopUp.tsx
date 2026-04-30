
import { Dialog, Button, Portal, CloseButton } from "@chakra-ui/react"

type PopUpProps = {
    message: string
    open: boolean
    setOpen: (open: boolean) => void
    confirmButtonMessage: string
    confirmAction: () => void
}

function PopUp({message, open, setOpen, confirmButtonMessage, confirmAction} : PopUpProps) { 
    return (
        <>
        <Dialog.Root lazyMount open={open} onOpenChange={(e) => setOpen(e.open)}>
        <Portal>
            <Dialog.Backdrop />
            <Dialog.Positioner>
            <Dialog.Content>
                <Dialog.Body paddingTop={'20px'}>
                <p>
                    {message}
                </p>
                </Dialog.Body>
                <Dialog.Footer>
                <Dialog.ActionTrigger asChild>
                    <Button variant="outline">Cancel</Button>
                </Dialog.ActionTrigger>
                <Button>{confirmButtonMessage}</Button>
                </Dialog.Footer>
                <Dialog.CloseTrigger asChild>
                <CloseButton size="sm" />
                </Dialog.CloseTrigger>
            </Dialog.Content>
            </Dialog.Positioner>
        </Portal>
        </Dialog.Root>
    </>
    )
}

export default PopUp