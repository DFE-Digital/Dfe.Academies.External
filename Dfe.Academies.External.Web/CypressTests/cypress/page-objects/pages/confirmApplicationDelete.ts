class ConfirmApplicationDelete {
  public confirmDelete(): this {
    cy.get('#deleteButton').should('be.visible').contains('Yes, delete').click()

    return this
  }
}

const confirmApplicationDelete = new ConfirmApplicationDelete()

export default confirmApplicationDelete
