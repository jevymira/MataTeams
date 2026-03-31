import { useParams } from "react-router"

function PublicProfile() {
    let { id} = useParams();

    return (
        <div>
            <p>{`Public profille: ${id}`}</p>
        </div>
    )
}

export default PublicProfile