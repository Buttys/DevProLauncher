--紋章の記録
function c37241623.initial_effect(c)
  --Activate
	local e1=Effect.CreateEffect(c)
	e1:SetCategory(CATEGORY_NEGATE+CATEGORY_DESTROY)
	e1:SetType(EFFECT_TYPE_ACTIVATE)
	e1:SetCode(EVENT_CHAINING)
	e1:SetCondition(c37241623.condition)
	e1:SetTarget(c37241623.target)
	e1:SetOperation(c37241623.activate)
	c:RegisterEffect(e1)
end

function c37241623.filter(c,re)
	return c:IsReason(REASON_COST) and c:IsPreviousLocation(LOCATION_OVERLAY)
	and c:GetReasonEffect()==re
end

function c37241623.condition(e,tp,eg,ep,ev,re,r,rp)
	local rc=re:GetHandler()
	return re:IsActiveType(TYPE_MONSTER) and Duel.IsChainNegatable(ev) and rc:IsType(TYPE_XYZ) and rc:IsControler(1-tp)
	and Duel.IsExistingMatchingCard(c37241623.filter,tp,LOCATION_GRAVE+LOCATION_REMOVED,LOCATION_GRAVE+LOCATION_REMOVED,1,nil,re)
end
function c37241623.target(e,tp,eg,ep,ev,re,r,rp,chk)
	if chk==0 then return true end
	Duel.SetOperationInfo(0,CATEGORY_NEGATE,eg,1,0,0)
	if re:GetHandler():IsDestructable() and re:GetHandler():IsRelateToEffect(re) then
		Duel.SetOperationInfo(0,CATEGORY_DESTROY,eg,1,0,0)
	end
end
function c37241623.activate(e,tp,eg,ep,ev,re,r,rp)
	if 	Duel.NegateActivation(ev) and re:GetHandler():IsRelateToEffect(re) then
		Duel.Destroy(eg,REASON_EFFECT)
	end
end
