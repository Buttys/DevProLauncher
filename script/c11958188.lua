--Bujingi Raven
function c11958188.initial_effect(c)
	--destroy
	local e1=Effect.CreateEffect(c)
	e1:SetDescription(aux.Stringid(11958188,1))
	e1:SetCategory(CATEGORY_DESTROY)
	e1:SetType(EFFECT_TYPE_FIELD+EFFECT_TYPE_TRIGGER_O)
	e1:SetProperty(EFFECT_FLAG_DAMAGE_STEP+EFFECT_FLAG_CARD_TARGET)
	e1:SetCode(EVENT_BATTLE_DESTROYED)
	e1:SetRange(LOCATION_HAND)
	e1:SetCondition(c11958188.con)
	e1:SetCost(c11958188.cost)
	e1:SetTarget(c11958188.tg)
	e1:SetOperation(c11958188.op)
	c:RegisterEffect(e1)
end
function c11958188.cfilter(c,tp)
	return c:IsControler(tp) and c:GetPreviousControler()==tp and c:IsSetCard(0x88) and c:IsType(TYPE_MONSTER)
		and c:IsReason(REASON_BATTLE) 
end
function c11958188.con(e,tp,eg,ep,ev,re,r,rp)
	local g=eg:Filter(c11958188.cfilter,nil,tp)
	e:SetLabelObject(g:GetFirst())
	return g:GetCount()>0
end
function c11958188.cost(e,tp,eg,ep,ev,re,r,rp,chk)
	if chk==0 then return e:GetHandler():IsAbleToGraveAsCost() end
	Duel.SendtoGrave(e:GetHandler(),REASON_COST)
end
function c11958188.tg(e,tp,eg,ep,ev,re,r,rp,chk)
	local c=e:GetHandler()
	local tc=e:GetLabelObject():GetReasonCard()
	if chk==0 then return true end
	tc:CreateEffectRelation(e)
	Duel.SetOperationInfo(0,CATEGORY_DESTROY,tc,1,0,0)
end
function c11958188.op(e,tp,eg,ep,ev,re,r,rp)
	local rc=e:GetLabelObject():GetReasonCard()
	if rc:IsRelateToEffect(e) then
		Duel.Destroy(rc,REASON_EFFECT)
	end
end
