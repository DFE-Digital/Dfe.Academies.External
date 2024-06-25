class WhatIsYourRole {
  // TODO get better selectors
  public chooseRole(roleType: string): this {
    const radio = roleType === 'Governor' ? 0 : 1
    cy.get('input[type=radio]').eq(radio).click()
    cy.get('input[type=radio]').eq(radio).should('be.checked')
    cy.get('input[type=submit]').click()

    return this
  }
}

const whatIsYourRole = new WhatIsYourRole()

export default whatIsYourRole
