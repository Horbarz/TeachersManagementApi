import React from 'react'

function IhsAlert({error,message}) {
    return (
        <div>
            <div className={error ? 'alert alert-danger alert-dismissible fade show' : 'alert alert-success alert-dismissible fade show'} role="alert">
                <i className={error ? 'mdi mdi-block-helper mr-2':'mdi mdi-check-all mr-2'}></i>{message}
                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
        </div>
    )
}

export default IhsAlert
