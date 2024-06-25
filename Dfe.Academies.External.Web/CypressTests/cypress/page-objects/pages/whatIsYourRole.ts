class WhatIsYourRole {
  public chooseRole(roleType: string): this {
    roleType = roleType === 'Governor' ? 'ChairOfGovernors' : 'Other'
    cy.get(`[id=RoleType${roleType}]`).click()
    cy.get(`[id=RoleType${roleType}]`).should('be.checked')
    cy.get('input[type=submit]').click()

    return this
  }
}

const whatIsYourRole = new WhatIsYourRole()

export default whatIsYourRole
