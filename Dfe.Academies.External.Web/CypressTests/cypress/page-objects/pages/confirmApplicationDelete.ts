class ConfirmApplicationDelete {
  public checkAppIDIsCorrectAndselectConfirmDelete(): this {
    cy.checkAppIDIsCorrectAndselectConfirmDelete()

    return this
  }
}

const confirmApplicationDelete = new ConfirmApplicationDelete()

export default confirmApplicationDelete
