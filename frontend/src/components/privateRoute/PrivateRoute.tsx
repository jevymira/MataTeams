import { ReactElement, useContext } from 'react'
import { UserContext } from '../../context/auth'
import { Navigate } from 'react-router'
import { UserContextType } from '../../types'

type ProtectedRouteProps = {
    outlet: ReactElement
}

function PrivateRoute({ outlet }: ProtectedRouteProps) {
    const { token } = useContext(UserContext) as UserContextType

    if (!token || token === '' || token.length < 1) {
        return <Navigate to="/login" />
    }
    return outlet

}

export default PrivateRoute