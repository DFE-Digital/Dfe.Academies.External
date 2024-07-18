class ConfirmInviteContributorDelete {
  public confirmRemoveContributor(): this {
    cy.get('[data-cy="confirmRemove"]').click()

    return this
  }
}

const confirmInviteContributorDelete = new ConfirmInviteContributorDelete()

export default confirmInviteContributorDelete
