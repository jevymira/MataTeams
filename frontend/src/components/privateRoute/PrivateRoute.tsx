import { ReactElement, useContext } from 'react'
import { AuthContext } from '../../context/auth'
import { Navigate } from 'react-router'
import { AuthContextType } from '../../types'

type ProtectedRouteProps = {
    outlet: ReactElement
}

function PrivateRoute({ outlet }: ProtectedRouteProps) {
    const { token } = useContext(AuthContext) as AuthContextType

    if (!token || token === '') {
        return <Navigate to="/login" />
    }
    return outlet

}

export default PrivateRoute