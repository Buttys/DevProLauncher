--ハーピィ・レディ－鳳凰の陣－
function c80900001.initial_effect(c)
	--Activate
	local e1=Effect.CreateEffect(c)
	e1:SetCategory(CATEGORY_DESTROY+CATEGORY_DAMAGE)
	e1:SetType(EFFECT_TYPE_ACTIVATE)
	e1:SetCode(EVENT_FREE_CHAIN)
	e1:SetCondition(c80900001.condition)
	e1:SetTarget(c80900001.target)
	e1:SetOperation(c80900001.activate)
	c:RegisterEffect(e1)
end
function c80900001.cfilter(c)
	return c:IsFaceup() and (c:IsCode(76812113) or c:IsCode(12206212))
end
function c80900001.condition(e,tp,eg,ep,ev,re,r,rp)
	return Duel.IsExistingMatchingCard(c80900001.cfilter,tp,LOCATION_MZONE,0,3,nil)
end
function c80900001.target(e,tp,eg,ep,ev,re,r,rp,chk)
	local c=e:GetHandler()
	if chk==0 then return Duel.IsExistingMatchingCard(Card.IsDestructable,tp,0,LOCATION_MZONE,1,nil) end
	local g=Duel.GetMatchingGroup(Card.IsDestructable,tp,0,LOCATION_MZONE,nil)
	local g1=Duel.GetMatchingGroup(c80900001.cfilter,tp,LOCATION_MZONE,0,nil)
	local count=g:GetCount()
	if g:GetCount()>g1:GetCount() then
		count=g1:GetCount()
	end
	Duel.Hint(HINT_SELECTMSG,tp,HINTMSG_DESTROY)
	local tg=Duel.SelectTarget(tp,Card.IsDestructable,tp,0,LOCATION_MZONE,count,count,nil)
	Duel.SetOperationInfo(0,CATEGORY_DESTROY,tg,tg:GetCount(),0,0)
	Duel.SetOperationInfo(0,CATEGORY_DAMAGE,nil,0,1-tp,0)
end
function c80900001.activate(e,tp,eg,ep,ev,re,r,rp)
local g=Duel.GetChainInfo(0,CHAININFO_TARGET_CARDS)
	local tg=g:Filter(Card.IsRelateToEffect,nil,e)
	tc=tg:GetFirst()
	local atk=0
	local dmg=0
	while tc do
		atk=tc:GetTextAttack()
		if atk>dmg then
			dmg=atk
		end
		Duel.Destroy(tc,REASON_EFFECT)
		tc=tg:GetNext()
	end
	Duel.Damage(1-tp,dmg,REASON_EFFECT)
end
