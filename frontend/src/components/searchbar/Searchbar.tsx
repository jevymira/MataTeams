// libraries
import { Input, InputGroup } from "@chakra-ui/react"
import { LuSearch } from "react-icons/lu"

function Searchbar() {
    return (
    <div style={{paddingRight:'15px', width: '500px'}}>
        <InputGroup startElement={<LuSearch />}>
        <Input placeholder="What kind of project do you want to work on?" />
        </InputGroup>
    </div>
    )
}

export default Searchbar